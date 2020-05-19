from actuators import ActuadoresAgropilot
from gps import GPSData
from db import DB
from time import sleep
import utils
global mode


control=ActuadoresAgropilot()
control.setup()
gps=GPSData()
gps.run()
sleep(1)
mode=gps.net.get_mode()
print("MODO INIT:",mode)

limite=[]
target, a, b = None
while True:
	while mode=="STOP":#FRENAR
		print("stopped")
		sleep(1)
		mode=gps.net.get_mode()
	while mode=="APAGAR":#APAGAR
		sleep(1)
		mode=gps.net.get_mode()
	while mode=="MANUAL":
		step,direccion=gps.net.get_test()
		print("ORDENES DE GIRO:",step,direccion)
		control.crear_giro(step,direccion)
		mode=gps.net.get_mode()
		sleep(1)
	if mode=="TARGET":
		target=utils.to_utm(gps.net.get_target())
		while mode=="TARGET":
			bearing=gps.nav
			target_course=utils.bearing(utils.to_utm(gps.pos()),target)
			dif=bearing-target
			r=gps.spd/gps.rad
			angulo=(1/r)*200
			print("angulo:",angulo)
			control.nav(dif,angulo)
			mode=gps.net.get_mode()
	if mode=="GRABAR LIMITE":
		limite=[]
		while mode=="GRABAR LIMITE":
			sleep(1)
			mode=gps.net.get_mode()
	if mode=="GRABAR A":
		sleep(1)
		mode=gps.net.get_mode()
	if mode=="GRABAR B":
		sleep(1)
		mode=gps.net.get_mode()
	if mode=="GRABAR B":
		sleep(1)
		mode=gps.net.get_mode()
	mode=gps.net.get_mode()
	sleep(1)







