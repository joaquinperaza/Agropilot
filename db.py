import firebase_admin
from firebase_admin import credentials
from firebase_admin import firestore


class DB:
	# Use the application default credentials
	global cred, db, status, conf
	firebase_admin.initialize_app()
	db = firestore.client()
	status = db.collection(u'status')
	conf = db.collection(u'conf')
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
<<<<<<< HEAD
		mode_doc = self.conf.collection(u'conf').document("net").get()
=======
		mode_doc = self.db.collection(u'conf').document("net").get()
		print(mode_doc.ip)
>>>>>>> 5ae64afcff61005e46b6d626ef7ef1852a9108df
		return mode_doc.ip

	def get_test(self):
		mode_doc = self.status.document("nav").get()
		return mode_doc.step, mode_doc.dir
