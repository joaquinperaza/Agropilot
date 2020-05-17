import firebase_admin
from firebase_admin import credentials
from firebase_admin import firestore


class DB:
	# Use the application default credentials
	def __init__(self):
		firebase_admin.initialize_app()
		self.db = firestore.client()
		self.status = db.collection(u'status')
		self.conf = db.collection(u'conf')
	def update(self):
		self.status.document("pos").update({u'lat': client.get('lat')})
		self.status.document("pos").update({u'lon': client.get('lon')})
		self.status.document("pos").update({u'sat': client.get('sat')})
		self.status.document("pos").update({u'age': client.get('age')})
		self.status.document("pos").update({u'spd': client.get('spd')})
		self.status.document("pos").update({u'nav': client.get('nav')})
		self.status.document("motor").update({u'dir': client.get('dir')})
		self.status.document("motor").update({u'step': client.get('step')})
		if client.get('acelerador')!=-1:
			self.status.document("servos").update({u'acelerador': client.get('acelerador')})
		self.status.document("servos").update({u'corte': client.get('corte')})

	def get_mode(self):
		mode_doc = self.status.document("nav").get()
		return mode_doc.mode

	def get_ip(self):
		mode_doc = self.conf.collection(u'conf').document("net").get()
		print(mode_doc.ip)
		return mode_doc.ip

	def get_test(self):
		mode_doc = self.status.document("nav").get()
		return mode_doc.step, mode_doc.dir
