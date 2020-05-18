from actuators import ActuadoresAgropilot
from gps import GPSData
from db import DB
from pymemcache.client import base
from time import sleep
import threading
global mode

client = base.Client(('localhost', 11211))
control=ActuadoresAgropilot()
control.setup()
gps=GPSData()
gps.run()
sleep(1)
mode=gps.net.get_mode()
print("MODO INIT:",mode)



while True:
	while mode=="STOP":
		print("stopped")
		sleep(1)
		mode=gps.net.get_mode()
	while mode=="MANUAL":
		step,direccion=gps.net.get_test()
		print("ORDENES DE GIRO:",step,direccion)
		control.crear_giro(step,direccion)
		mode=gps.net.get_mode()
		sleep(1)
	while mode=="recording":
		step,direccion=db.get_test()
		client.set('step',step)
		client.get('dir',direccion)
		mode=db.get_mode()







