from vpython import *
from coordinates import Coordinate
from pyproj import Geod, CRS, transform
import coords, utils
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
left=curve( color=color.red, radius=0.3, retain=500)
right=curve( color=color.red, radius=0.3, retain=500)
def move(LatLon):
	global old_pos, init, old_bearing, mesh, bearings
	y,x=transform(wgs84, UTM, LatLon.y, LatLon.x)
	utm_pos=Coordinate(x=x,y=y)
	if init is False:
		init=True
		old_pos=utm_pos
		scene.camera.pos=vec(utm_pos.x, 4, utm_pos.y-50)
		scene.camera.axis=vec(0, -4 , -50)
		obj=[]
		for x in range(-50, 50):
			for y in range(-50, 50):
				print(x,y)
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
		scene.camera.pos=vec(camera.x, 100, camera.y)
		x,z=utils.vector(curso+180)
		scene.camera.axis=200*vec(x, -0.3 , z)



	     

coords=coords.coords()
print(utils.bearing(Coordinate(x=0,y=0),Coordinate(x=1,y=1)))#45
print(utils.bearing(Coordinate(x=-1,y=0),Coordinate(x=1,y=0)))#90
print(utils.bearing(Coordinate(x=0,y=1),Coordinate(x=0,y=-1)))#180
print(utils.offset(Coordinate(x=0,y=0),0,1))
print(utils.offset(Coordinate(x=0,y=0),90,1))
print(utils.offset(Coordinate(x=0,y=0),180,1))
print(utils.offset(Coordinate(x=0,y=0),270,1))
for c in coords:
	move(c)
