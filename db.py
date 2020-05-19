
import firebase_admin
from firebase_admin import credentials
from firebase_admin import firestore
from pymemcache.client import base
from coordinates import Coordinate



def update_modo(doc_snapshot, changes, read_time):
	try:
		client = base.Client(('localhost', 11211))
		modo=doc_snapshot.to_dict()["mode"]
		client.set('mode',modo)
	except Exception as e:
		print("Error actualizar modo", repr(e))

class DB:
	# Use the application default credentials
	def __init__(self):
		firebase_admin.initialize_app()
		self.db = firestore.client()
		self.status = self.db.collection(u'status')
		self.conf = self.db.collection(u'conf')
		self.client = base.Client(('localhost', 11211))
		self.mode_watch = status.document("nav").on_snapshot(update_modo)

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
	
	def get_target(self):
		mode_doc = self.status.document("nav").get().to_dict()
		target=Coordinate( float(mode_doc["lat"]) , float(mode_doc["lon"]) )
		return target

	def get_ip(self):
		mode_doc = self.conf.document("params").get().to_dict()
		return mode_doc["ip"]

	def get_test(self):
		mode_doc = self.status.document("nav").get().to_dict()
		return mode_doc["step"], mode_doc["dir"]
