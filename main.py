from actuators import ActuadoresAgropilot
from gps import GPSData
from db import DB
from pymemcache.client import base
from time import sleep

global mode

client = base.Client(('localhost', 11211))
control=ActuadoresAgropilot()
control.setup()
gps=GPSData()
gps.run()
sleep(1)
mode=gps.net.get_mode()
print("MODO INIT:",mode)

def update():
	try:
		gps.net.update()
	except Exception as e:
		print("UPDATE ERR",repr(e))
while True:
	while mode=="stop":
		mode=gps.net.get_mode()
		print("stopped")
		sleep(.05)
		update()
	while mode=="test":
		step,direccion=gps.net.get_test()
		print("ordenado", str(step),str(dir))
		client.set('step',str(step))
		client.set('dir',str(direccion))
		mode=gps.net.get_mode()
		print("Fin test a -> ",mode)
		sleep(1)
		update()
	while mode=="recording":
		step,direccion=db.get_test()
		client.set('step',step)
		client.get('dir',direccion)
		mode=db.get_mode()
		sleep(1)
		update()







