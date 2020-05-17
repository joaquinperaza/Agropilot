from actuators import ActuadoresAgropilot
from gps import GPSData
from db import DB
from pymemcache.client import base
from time import sleep

client = base.Client(('localhost', 11211))
control=ActuadoresAgropilot()
control.setup()
gps=GPSData()
gps.run()
net=new DB()
db.update()
mode=db.get_mode()

while True:
	while mode=="test":
		step,direccion=db.get_test()
		client.set('step',step)
		client.get('dir',direccion)
		mode=db.get_mode()
		sleep(1)
		db.update()
	while mode=="recording":
		step,direccion=db.get_test()
		client.set('step',step)
		client.get('dir',direccion)
		mode=db.get_mode()
		sleep(1)
		db.update()







