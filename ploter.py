from vpython import *
from coordinates import Coordinate
from pyproj import Geod, CRS, transform
import coords as coords
import utils as utils
import nav as nav
from tractor import Tractor
import time
from shapely.geometry import Point, LineString, LinearRing, Polygon
import numpy as np
from random import random, randrange

g = Geod(ellps='WGS84')
wgs84=CRS("EPSG:4326") # LatLon with WGS84 datum used by GPS units and Google Earth 
UTM=CRS("EPSG:32721") # UTM coords, zone 26N, WGS84 datum 
init = False
old_pos=False
c=False
distance=19
Coordinate.default_order = 'yx' #LatLng
scene = canvas(width=1200, height=700, background=color.white)
old_bearing = False
bearings=[0,0,0,0,0,0,0,0,0,0,0]
mesh=[]
left=curve( color=color.orange, radius=0.3, retain=500)
right=curve( color=color.orange, radius=0.3, retain=500)
ab=curve( color=color.black, radius=0.5, retain=500)
limit_line=curve( color=color.white, radius=1,)
rate (5)
tractor = Tractor()
tractor_3d=box( color=color.red, size=vec(1,1,1) )
def move(LatLon):
	global old_pos, init, old_bearing, mesh, bearings, tractor
	utm_pos=Coordinate(x=LatLon.x,y=LatLon.y)
	if init is False:
		tractor.m_p_s=2
		tractor.run()
		init=True
		old_pos=utm_pos
		scene.camera.pos=vec(utm_pos.x, 4, utm_pos.y-50)
		scene.camera.axis=vec(0, -4 , -50)
		obj=[]
		for x in range(-50, 50):
			for y in range(-50, 50):
				a=Coordinate(x=utm_pos.x+x*500, y=utm_pos.y+y*500+500)
				b=Coordinate(x=utm_pos.x+x*500+500, y=utm_pos.y+y*500+500)
				c=Coordinate(x=utm_pos.x+x*500+500, y=utm_pos.y+y*500)
				d=Coordinate(x=utm_pos.x+x*500, y=utm_pos.y+y*500)
				Q = quad( canvas=None, v0=vertex(pos=vec(a.x,-1,a.y)), v1=vertex(pos=vec(b.x,-1,b.y)), v2=vertex(pos=vec(c.x,-1,c.y)), v3=vertex(pos=vec(d.x,-1,d.y)),   )
				obj.append(Q)
		compound(obj, canvas=scene, color=vec(0,.60,0))
		

	else:
		curso=utils.bearing(utm_pos,old_pos)
		a=utils.offset(utm_pos,curso-90,distance/2)
		b=utils.offset(utm_pos,curso+90,distance/2)
		c=utils.offset(old_pos,old_bearing+90,distance/2)
		d=utils.offset(old_pos,old_bearing-90,distance/2)
		Q = quad(v0=vertex(pos=vec(a.x,.5,a.y), color=color.yellow), v1=vertex(pos=vec(b.x,.5,b.y), color=color.yellow), v2=vertex(pos=vec(c.x,.5,c.y), color=color.yellow), v3=vertex(pos=vec(d.x,.5,d.y), color=color.yellow),  )
		left.append(vec(a.x,.5,a.y))
		right.append(vec(b.x,.5,b.y))
		old_bearing=curso
		old_pos=utm_pos
		bearings.pop()
		bearings.insert(0,curso)
		curso=sum(bearings) / len(bearings) 
		camera=utils.offset(utm_pos,curso,300)
		
		x,z=utils.vector(curso+180)
		#scene.camera.pos=vec(camera.x, 100, camera.y)
		#scene.camera.axis=200*vec(x, -0.3 , z)
		tractor_3d.pos=tractor.get_vec(1.5)


coords2=coords.coords()

path = nav.create_path(coords.a, coords.b, coords2,19,reverse=True,dir=0)
for c in path:
	ab.append(vec(c.x,.5,c.y))

tractor.location=path[0]
while True:
	move(tractor.location)
	print(tractor.location," Bearing:",tractor.bearing)
	d=(Point(tractor.location.x,tractor.location.y))
	point,dist,wp_bearing=nav.get_target(d,path)
	direct_bearing=utils.bearing(d,point)
	target=nav.get_target_course(direct_bearing, wp_bearing, dist, 10)
	tractor.doblar((target-tractor.bearing))
	print("Dist: ","{0:0.2f}".format(dist),"m. WP_course: ","{0:0.2f}".format(wp_bearing),"deg. Direct_course: ","{0:0.2f}".format(direct_bearing),"deg. Target: ","{0:0.2f}".format(target),"deg.")
	#print("{0:0.2f}".format(c.x),";","{0:0.2f}".format(c.y))
	camera=utils.offset(tractor.location,tractor.bearing,-250)
	x,z=utils.vector(tractor.bearing)
	scene.camera.pos=vec(camera.x, 80, camera.y)
	scene.camera.axis=150*vec(x, -0.3 , z)
	if(randrange(0,10)==5):
		tractor.juego_en_la_direccion()

	



		

