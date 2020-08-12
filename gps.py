import io
import math
import pynmea2
import threading
from pymemcache.client import base
import socket
from db import DB
import time
import utils
from coordinates import Coordinate
from datetime import date, datetime, timedelta
from shapely.geometry import Point
Coordinate.default_order = 'yx'

class TCPConnection:
    def __init__(self, sock=None):
        if sock is None:
            self.sock = socket.socket(
                            socket.AF_INET, socket.SOCK_STREAM)
        else:
            self.sock = sock

    def connect(self, host, port):
        try:
            self.sock.connect((host, port))
            print('Successful Connection')
        except:
            print('Connection Failed')

    def close(self):
        self.sock.close()
        print("closed")

    def readlines(self):
        try:
            data = self.sock.recv(1024).decode('utf-8')
            return data.split("\r\n")
        except:
            self.sock.close()
            return None


class GPSData:
    def __init__(self):
        self.net= DB()
        self.listen = TCPConnection()
        self.client = base.Client(('localhost', 11211))
        self.nav=0
        self.spd=0
        self.lat=0
        self.lon=0
        self.rad=0
        self.ip=self.find_ip()
        self.last=datetime.now().timestamp()
        self.listen.connect(self.ip,8888)
        self.t1=None
    def find_ip(self):
        s=socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        s.connect(("8.8.8.8", 80))
        myip=s.getsockname()[0].split(".")[0:3]
        ip=myip[0]+"."+myip[1]+"."+myip[2]+"."
        s.close()
        test=socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        try:
            test.connect((self.net.get_ip(), 8888))
            test.close()
            print("OLD IP")
            return self.net.get_ip()
        except:
            print("NEW IP LOOKING...")
        for x in range(99,121):
            try:
                test.connect((ip+str(x), 8888))
                test.close()
                self.net.set_ip(ip+str(x))
                return ip+str(x)
            except Exception as e:
                print(x,repr(e))
        retry=self.find_ip()
        return retry

    def runner_child(self):
        err=0
        while True:
            try:
                lines = self.listen.readlines()
                for line in lines:
                    if (line.startswith("$GNGGA")):
                        err=0
                        data = pynmea2.parse(line)
                        self.lat=data.latitude
                        self.lon=data.longitude
                        self.client.set('lat',str(data.latitude))
                        self.client.set('lon', str(data.longitude))
                        self.client.set('sat', str(data.num_sats))
                        self.client.set('age', str(data.age_gps_data))

                    elif (line.startswith("$GNRMC")):
                        err=0
                        data = pynmea2.parse(line)
                        self.spd=data.spd_over_grnd*0.5144
                        tstamp=datetime.combine(data.datestamp, data.timestamp).timestamp()
                        true_course=data.true_course or 0
                        self.nav=true_course
                        self.lat=data.latitude
                        self.lon=data.longitude
                        self.client.set('spd', str(data.spd_over_grnd*0.5144))
                        self.client.set('nav', str(true_course))
                        self.last=tstamp
                    else:
                        err+=1
                    if err>200:
                        err=0
                        raise Exception('err', 'con')
            except Exception as e:
                print("GPS error", repr(e))
                try:
                    self.listen.close()
                    time.sleep(10)
                    self.listen = TCPConnection()
                    time.sleep(2)
                    self.listen.connect(self.ip,8888)
                    time.sleep(5)
                except:
                    print("reconnect error")
    def pos(self):
        return Coordinate( self.lat , self.lon )
    def point(self):
        p=utils.to_utm(self.pos())
        return Point(p.x,p.y)
    def run(self):
        self.t1 = threading.Thread(target=self.runner_child)
        self.t1.start()
    def stop(self):
        self.t1.stop()
