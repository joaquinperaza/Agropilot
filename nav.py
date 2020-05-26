from coordinates import Coordinate
import math
import coords, utils
from shapely.geometry import Point, LineString, LinearRing, Polygon
from shapely.ops import nearest_points
import numpy as np


#Reverse
#90 course sign change
#left to right
# -1 1 change
def create_path(a,b,contour,distancia,dir=0):
    conf_1=-90
    conf_2=-1
    conf_3="left"
    conf_4=0
    if dir==1:
        conf_3="right"
    path=[]
    contorno_points=[]
    for c in contour:
        c=utils.to_utm( c)
        utm_pos=Point(c.x,c.y)
        contorno_points.append(utm_pos)
    a2=utils.to_utm(a)
    a_utm=Point(a2.x,a2.y)
    b2=utils.to_utm(b)
    b_utm=Point(b2.x,b2.y)
    ab_course=utils.bearing(a_utm,b_utm)
    a_utm=utils.offset(a2,ab_course-90,distancia*80)
    b_utm=utils.offset(b2,ab_course-90,distancia*80)
    a2,b2=utils.extend(a_utm,b_utm)
    AB= LineString([a2,b2])
    contorno=Polygon(contorno_points)
    eroded=contorno.buffer(-distancia, resolution=16, join_style=1)
    line=AB.intersection(eroded)


    for x in range(1, 200):
        ab_1=AB.parallel_offset(x*distancia, conf_3)
        line=ab_1.intersection(eroded)
        if(line.geom_type=="LineString"):
            if(len(line.coords)>1):
                p1=1
                p2=conf_2
                if x%2==0:
                    p1=0
                    p2=-conf_2
                centro=utils.offset(utils.toCoord(line.coords[p1]),ab_course+conf_1,distancia/2)
                radius = distancia/2
                start_angle, end_angle = 90-ab_course-90, 90-ab_course+90 # In degrees
                if x%2==0:
                    start_angle, end_angle = 90-ab_course+90, 90-ab_course-90 
                numsegments = 200
                theta = np.radians(np.linspace(start_angle, end_angle, numsegments))
                x = centro.x + (radius * np.cos(theta))*p2
                y = centro.y + (radius * np.sin(theta))*p2
                arc = LineString(np.column_stack([x, y]))
                for c in arc.coords:
                        path.append(Point(c))
    final=LineString(path)
    path2=[]
    for x in range(0, int(final.length/2)):
        p=final.interpolate(x*2)
        path2.append(p)
    return path2


def get_target(gps, path):
    d=1000000
    p_min=None
    for p in path:
        _d=gps.distance(p)
        if _d < d:
            d=_d
            p_min=p
    nearest_A=p_min
    nearest_B=p_min
    print("I:", path.index(p_min))
    if path.index(p_min)<len(path)-6:
        nearest_B=path[path.index(p_min)+6]
    return nearest_B, d

def get_target_course(b_to_target, target_bearing, d, max_d):
    p = d / max_d
    if p > 1:
        p = 1
    direct_factor = 0.1 + 0.5 * pow((10 * (p)), 0.25)
    indirect_factor = 1 - direct_factor
    return b_to_target * direct_factor + target_bearing * indirect_factor

def cross_err(position, nav, path):
   punto=nearest_points(position, path)[1]
   curso=utils.bearing(position,punto)
   lado=utils.get_diff(nav,curso)
   distancia=punto.distance(position)
   distancia=math.copysign(distancia,lado)
   return distancia


