
from actuators import ActuadoresAgropilot
from gps import GPSData
from db import DB
from time import sleep
import utils, sys
import navigator
import nav as nav_utils
global mode
import traceback
from shapely.geometry import LineString

control=ActuadoresAgropilot()
gps=GPSData()
gps.run()
gps.net.run()
sleep(1)

mode=gps.net.get_mode("")
print("MODO INIT:",mode)
global limite, target, a, b, route
limite=[]
route=[]
target, a, b = None, None, None
while True:
	try:
		while mode=="STOP":#FRENAR
			print("stopped")
			sleep(1)
			mode=gps.net.get_mode(mode)
		while mode=="APAGAR":#APAGAR
			sleep(1)
			mode=gps.net.get_mode(mode)
		while mode=="MANUAL":
			step,direccion=gps.net.get_test()
			print("ORDENES DE GIRO:",step,direccion)
			control.crear_giro(step,direccion)
			mode=gps.net.get_mode(mode)
			sleep(1)
		if mode=="TARGET":
			target=utils.to_utm(gps.net.get_target())
			tractor=navigator.Tractor()
			while mode=="TARGET":
				bearing=gps.nav
				target_course=utils.bearing(utils.to_utm(gps.pos()),target)
				dif=utils.get_diff(bearing,target_course)
				print("bearing",bearing,"target",target_course,"dif",dif)
				calc=tractor.doblar(dif,0)
				print(calc)
				control.crear_giro(calc)
				mode=gps.net.get_mode(mode)
				sleep(.15)
		if mode=="GRABAR LIMITE":
			limite=[]
			gps.net.clear_mission()
			while mode=="GRABAR LIMITE":
				try:
					c=gps.pos()
					limite.append(c)
					mode=gps.net.get_mode(mode)
					sleep(1)
				except Exception as e:
					print("MASTER ERR", repr(e))
			gps.net.set_limit(limite)
		if mode=="GRABAR A":
			a=gps.pos()
			gps.net.set_a(a)
			mode=gps.net.get_mode(mode)
			sleep(1)
		if mode=="GRABAR B":
			b=gps.pos()
			gps.net.set_b(b)
			mode=gps.net.get_mode(mode)
			sleep(1)
		if mode=="CREAR RUTA":
			if len(limite)<2:
				limite,a,b=gps.net.load_limit()
			sleep(1)
			mode=gps.net.set_mode("STOP")
			au=utils.to_utm(a)
			bu=utils.to_utm(b)
			print(au,bu)
			curso=utils.bearing(au,bu)
			dir2= 1 if curso<180 else 0
			route=nav_utils.create_path(a, b, limite,gps.net.get_ancho(),dir=dir2)
			print(limite)
			gps.net.set_wp(route)
			mode=gps.net.get_mode(mode)
			sleep(1)
		if mode=="AUTO":
			tractor=navigator.Tractor()
			if len(route)<2:
				route,a,b=gps.net.load_route()
			ruta=LineString(route)
			control.reset()
			control.centro=gps.net.get_centro()
			while mode=="AUTO":
				bearing=gps.nav
				dif=nav_utils.cross_err(gps.point(),bearing,ruta)
				calc=tractor.doblar(dif)
				print("CROSSTRACK:",dif,"PID:",calc)
				control.crear_giro(calc)
				mode=gps.net.get_mode(mode)
				sleep(.2)
				try:
					gps.net.set_actual(control.leer_giro())
				except:
					print("Err steer sensr")
		if mode=="AUTOOLD":
			tractor=navigator.Tractor()
			if len(route)<2:
				route=gps.net.load_route()
			while mode=="AUTOOLD":
				target,dist=nav_utils.get_target(gps.point(),route)
				bearing=gps.nav
				target_course=utils.bearing(utils.to_utm(gps.pos()),target)
				dif=utils.get_diff(bearing,target_course)
				print("bearing",bearing,"target",target_course,"dif",dif)
				calc=tractor.doblar(dif,0)
				print(calc)
				control.crear_giro(int(calc[0]),calc[1])
				mode=gps.net.get_mode(mode)
				sleep(.2)
		sleep(1)
		mode=gps.net.get_mode()
	except KeyboardInterrupt:
		sleep(5)
		sys.exit()
	except Exception as e:
		print("MASTER ERR", repr(e))
		traceback.print_exc()







