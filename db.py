
import firebase_admin
from firebase_admin import credentials
from firebase_admin import firestore
from pymemcache.client import base
from coordinates import Coordinate
from time import sleep
import utils
Coordinate.default_order = 'yx'
import threading


class DB:
	# Use the application default credentials
	def __init__(self):
		firebase_admin.initialize_app()
		self.db = firestore.client()
		self.status = self.db.collection(u'status')
		self.conf = self.db.collection(u'conf')
		self.mission = self.db.collection(u'mission')
		self.client = base.Client(('localhost', 11211))
		self.mode_watch = status.document("nav").on_snapshot(update_modo)
		self.mode_watch2 = conf.document("params").on_snapshot(update_conf)
	
	@staticmethod
	def update_modo(doc_snapshot, changes, read_time):
		try:
			client = base.Client(('localhost', 11211))
			modo=doc_snapshot.to_dict()["mode"]
			client.set('mode',modo)
		except Exception as e:
			print("Error actualizar modo", repr(e))

	@staticmethod
	def update_conf(doc_snapshot, changes, read_time):
		try:
			client = base.Client(('localhost', 11211))
			params=doc_snapshot.to_dict()
			client.set('p', params["p"])
			client.set('i', params["i"])
			client.set('d', params["d"])
			client.set('ancho',params["ancho"])
		except Exception as e:
			print("Error actualizar conf", repr(e))


	def get_key_float(self,key):
		valor=None
		try:
			valor=float(self.client.get(key))
		except:
			valor=-2

	def update(self):
		data={
			u'lat': float(self.get_key_float('lat')),
			u'lon': float(self.get_key_float('lon')),
			u'sat': float(self.get_key_float('sat')),
			u'age': float(self.get_key_float('age')),
			u'spd': float(self.get_key_float('spd')),
			u'nav': float(self.get_key_float('nav')),
			u'm1_dir': int(self.get_key_float('dir')),
			u'm1_stp': int(self.get_key_float('step')),
			u's1_acl': int(self.get_key_float('acel')),
			u's2_cte': int(self.get_key_float('corte')),
			u'modo': self.client.get('mode'),
			u'timestamp': firestore.SERVER_TIMESTAMP
		}
		self.status.document("data").update(data)
	def oldget_mode(self):
		mode_doc = self.status.document("nav").get().to_dict()
		self.client.set('mode',mode_doc["mode"])

	def get_mode(self):
		return self.client.get('mode')
	
	def set_mode(self,modo):
		self.status.document("nav").update({"mode":modo})
		sleep(1)

	def get_ancho(self):
		return int(self.client.get('mode'))
	
	def get_target(self):
		mode_doc = self.status.document("nav").get().to_dict()
		target=Coordinate( float(mode_doc["lat"]) , float(mode_doc["lon"]) )
		return target

	def get_ip(self):
		mode_doc = self.conf.document("params").get().to_dict()
		return mode_doc["ip"]

	def get_test(self):
		mode_doc = self.status.document("nav").get().to_dict()
		self.status.document("nav").update({"step": "0", "dir": "0"})
		return mode_doc["step"], mode_doc["dir"]

	def add_limit(self,coord):
		coord2 = firestore.GeoPoint(coord.y, coord.x)
		self.mission.document("routes").update({u'limit': firestore.ArrayUnion([coord2])})
	
	def set_wp(self,coords):
		nav=[]
		for cord in coords:
			coord=utils.to_wgs84(cord)
			nav.append(firestore.GeoPoint(coord.y, coord.x))
		self.mission.document("routes").update({u'nav': nav})
	
	def set_a(self,coord):
		coord2 = firestore.GeoPoint(coord.y, coord.x)
		self.client.set('a_lat',str(coord.y))
		self.client.set('a_lon',str(coord.x))
		self.mission.document("routes").update({u'a': coord2})
		self.status.document("nav").update({"mode":"STOP"})

	def set_b(self,coord):
		coord2 = firestore.GeoPoint(coord.y, coord.x)
		self.client.set('b_lat',str(coord.y))
		self.client.set('b_lon',str(coord.x))
		self.mission.document("routes").update({u'b': coord2})
		self.status.document("nav").update({"mode":"STOP"})

	def clear_mission(self):
		self.mission.document("routes").set({"nav": [], "limit": [],"a": None, "b": None})
	
	def clear_mission_wo(self):
		self.mission.document("routes").update({"nav": []})

	def update_child(self):
		while True:
			try:
				self.update()
				sleep(2)

	def run(self):
		t1 = threading.Thread(target=self.update_child)
		t1.start()