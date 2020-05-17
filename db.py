
import firebase_admin
from firebase_admin import credentials
from firebase_admin import firestore
from pymemcache.client import base

class DB:
	# Use the application default credentials
	def __init__(self):
		firebase_admin.initialize_app()
		self.db = firestore.client()
		self.status = self.db.collection(u'status')
		self.conf = self.db.collection(u'conf')
		self.client = base.Client(('localhost', 11211))

	def update(self):
		self.status.document("pos").update({u'lat': self.client.get('lat')})
		self.status.document("pos").update({u'lon': self.client.get('lon')})
		self.status.document("pos").update({u'sat': self.client.get('sat')})
		self.status.document("pos").update({u'age': self.client.get('age')})
		self.status.document("pos").update({u'spd': self.client.get('spd')})
		self.status.document("pos").update({u'nav': self.client.get('nav')})
		self.status.document("motor").update({u'dir': self.client.get('dir')})
		self.status.document("motor").update({u'step': self.client.get('step')})
		if int(self.client.get('acel'))!=-1:
			self.status.document("servos").update({u'acel': self.client.get('acelerador')})
		self.status.document("servos").update({u'corte': self.client.get('corte')})
	def get_mode(self):
		mode_doc = self.status.document("nav").get().to_dict()
		print(mode_doc)
		return mode_doc["mode"]

	def get_ip(self):
		mode_doc = self.conf.document("net").get().to_dict()
		print(mode_doc)
		return mode_doc["ip"]

	def get_test(self):
		mode_doc = self.status.document("nav").get().to_dict()
		return mode_doc["step"], mode_doc["dir"]
