from coordinates import Coordinate
from pyproj import Geod, CRS, transform
import coords, utils
from shapely.geometry import Point, LineString, LinearRing, Polygon
import numpy as np

g = Geod(ellps='WGS84')
wgs84=CRS("EPSG:4326") # LatLon with WGS84 datum used by GPS units and Google Earth 
UTM=CRS("EPSG:32721")

#Reverse
#90 course sign change
#left to right
# -1 1 change
def create_path(a,b,contour,distancia,reverse=False,dir=0):
    conf_1=-90
    conf_2=1
    conf_3="left"
    conf_4=0
    if dir==1:
        conf_3="right"
    if reverse:
        conf_1=90
        conf_2=-1
        conf_3="right"
        conf_4=1
        if dir==1:
            conf_3="left"
    path=[]
    contorno_points=[]
    for c in contour:
	    xx,yy=transform(wgs84, UTM, c.y, c.x)
	    utm_pos=Coordinate(x=xx,y=yy)
	    contorno_points.append(Point(utm_pos.x,utm_pos.y))
    x,y=transform(wgs84, UTM, a.y, a.x)
    a_utm=Point(x,y)
    x,y=transform(wgs84, UTM, coords.b.y, coords.b.x)
    b_utm=Point(x,y)
    a2,b2=utils.extend(a_utm,b_utm)
    ab_course=utils.bearing(a_utm,b_utm)
    AB= LineString([a2,b2])
    contorno=Polygon(contorno_points)
    eroded=contorno.buffer(-distancia, resolution=16, join_style=1)
    line=AB.intersection(eroded)
    for c in line.coords:
        path.append(Point(c))
    centro=utils.offset(utils.toCoord(line.coords[conf_4]),ab_course+conf_1,distancia/2)
    radius = distancia/2
    start_angle, end_angle = 90-ab_course+90, 90-ab_course-90 # In degrees
    numsegments = 200
    theta = np.radians(np.linspace(start_angle, end_angle, numsegments))
    x = centro.x + (radius * np.cos(theta))*conf_2*-1
    y = centro.y + (radius * np.sin(theta))*conf_2*-1
    arc = LineString(np.column_stack([x, y]))
    for c in arc.coords:
            path.append(Point(c))

    for x in range(1, 30):
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
    if path.index(p_min)<len(path)-1:
        nearest_B=path[path.index(p_min)+1]
    curso=utils.bearing(nearest_A,nearest_B)
    return nearest_B, d, curso

def get_target_course(b_to_target, target_bearing, d, max_d):
    p = d / max_d
    if p > 1:
        p = 1
    direct_factor = 0.1 + 0.5 * pow((10 * (p)), 0.25)
    indirect_factor = 1 - direct_factor
    return b_to_target * direct_factor + target_bearing * indirect_factor

