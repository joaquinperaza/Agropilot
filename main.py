from actuators import ActuadoresAgropilot
from gps import GPSData
from db import DB
from time import sleep
import utils
import navigator
import nav as nav_utils
global mode


control=ActuadoresAgropilot()
control.setup()
gps=GPSData()
gps.run()
gps.net.run()
sleep(1)

mode=gps.net.get_mode()
print("MODO INIT:",mode)
global limite, target, a, b, route
limite=[]
route=[]
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
		tractor=navigator.Tractor()
		while mode=="TARGET":
			bearing=gps.nav
			target_course=utils.bearing(utils.to_utm(gps.pos()),target)
			dif=bearing-target
			r=gps.spd/gps.rad
			angulo=(1/r)*200
			print("angulo:",angulo)
			print("dif:",dif)
			control.crear_giro(tractor.doblar(dif,angulo))
			mode=gps.net.get_mode()
	if mode=="GRABAR LIMITE":
		limite=[]
		gps.net.clear_mission()
		while mode=="GRABAR LIMITE":
			c=gps.get_pos()
			limite.append(c)
			gps.net.add_limit(c)
			mode=gps.net.get_mode()
			sleep(1)
	if mode=="GRABAR A":
		a=gps.pos()
		gps.net.set_a(a)
		mode=gps.net.get_mode()
		sleep(1)
	if mode=="GRABAR B":
		b=gps.pos()
		gps.net.set_b(b)
		mode=gps.net.get_mode()
		sleep(1)
	if mode=="CREAR RUTA":
		sleep(1)
		mode=gps.net.set_mode("STOP")
		gps.net.clear_mission()
		route=nav_utils.create_path(a, b, limite,gps.net.get_ancho(),reverse=True,dir=0)
		gps.net.set_wp(route)
		mode=gps.net.get_mode()
		sleep(1)
	sleep(1)
	mode=gps.net.get_mode()







