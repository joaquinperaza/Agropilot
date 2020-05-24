from coordinates import Coordinate
import math
from shapely.geometry import Point, LineString
from pyproj import Geod, Proj, transform




Coordinate.default_order = 'yx'
UTM = Proj(init='epsg:32721')
wgs84 = Proj(init="epsg:4326")

def to_utm(c):
    xx,yy=UTM(c.x, c.y)
    return Coordinate(x=xx,y=yy)
def to_wgs84(c):
    xx,yy=transform(wgs84, UTM, c.y, c.x)
    return Coordinate(xx,yy)


def get_diff(init, final):
    if init > 360 or init < 0 or final > 360 or final < 0:
        raise Exception("out of range")
    diff = final - init
    absDiff = abs(diff)
    if absDiff == 180:
        return absDiff
    elif absDiff < 180:
        return diff
    elif final > init:
        return absDiff - 360
    else:
        return 360 - absDiff


def degree(x):
    pi=math.pi
    degree=(x*180)/pi
    return degree

def radians(x):
    pi=math.pi
    rad=(x*pi)/180
    return rad

def offset(point,angle,d):
    xx = point.x + ( d * math.cos(radians(90-angle)) )
    yy = point.y + ( d * math.sin(radians(90-angle)) )
    return Point(xx, yy)

def vector(angle):
    return  math.cos(radians(90-angle)),  math.sin(radians(90-angle))

def bearing(b,a):
    dx = a.x-b.x
    dy = a.y-b.y
    bearing=90-degree(math.atan2(dy,dx))
    if bearing<0:
        bearing+=360
    return bearing

def extend(a,b):
    m = (a.y-b.y)/(a.x-b.x)
    _b= (a.x*b.y - b.x*a.y)/(a.x-b.x)
    #y=mx+b
    def ye(x):
        return m*x+_b
    return Point(a.x-10000, ye(a.x-10000)), Point(b.x+10000, ye(b.x+10000))

def toCoord(point):
    return Coordinate(x=point[0],y=point[1])
    
