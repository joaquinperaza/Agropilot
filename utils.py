from coordinates import Coordinate
import math
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
    return Coordinate(x=xx, y=yy)

def vector(angle):
    return  math.cos(radians(90-angle)),  math.sin(radians(90-angle))
def bearing(b,a):
    dx = a.x-b.x
    dy = a.y-b.y
    return 90-degree(math.atan2(dy,dx))
