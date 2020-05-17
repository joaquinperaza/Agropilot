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

def update():
	try:
		t1 = threading.Thread(target=gps.net.update)
		t1.start()
		t2 = threading.Thread(target=gps.net.get_mode)
		t2.start()
	except Exception as e:
		print("UPDATE ERR",repr(e))

while True:
	while mode=="stop":
		print("stopped")
		update()
		sleep(1)
		mode=gps.net.fast_get_mode()
	while mode=="test":
		step,direccion=gps.net.get_test()
		print("Giro ordenado", str(step),str(dir))
		client.set('step',str(step))
		client.set('dir',str(direccion))
		update()
		mode=gps.net.fast_get_mode()
		print("Fin test a -> ",mode)
	while mode=="recording":
		step,direccion=db.get_test()
		client.set('step',step)
		client.get('dir',direccion)
		mode=db.get_mode()
		update()







