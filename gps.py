import io
import math
import pynmea2
import threading
from pymemcache.client import base
import socket
from db import DB
import time
from coordinates import Coordinate
from datetime import date, datetime, timedelta

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

    def readlines(self):
        data = self.sock.recv(1024).decode('utf-8')
        return data.split("\r\n")


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
        self.last=datetime.now()
        self.listen.connect(self.net.get_ip(),8888)
        self.t1=None
    def runner_child(self):
        while True:
            try:
                lines = self.listen.readlines()
                for line in lines:
                    if (line.startswith("$GNGGA")):
                        data = pynmea2.parse(line)
                        self.lat=data.latitude
                        self.lon=data.longitude
                        self.client.set('lat',str(data.latitude))
                        self.client.set('lon', str(data.longitude))
                        self.client.set('sat', str(data.num_sats))
                        self.client.set('age', str(data.age_gps_data))

                    if (line.startswith("$GNRMC")):
                        data = pynmea2.parse(line)
                        self.spd=data.spd_over_grnd*0.5144
                        tstamp=datetime.combine(data.datestamp, data.timestamp)
                        if(self.last!=tstamp):
                            self.rad=((data.true_Course-self.nav)*math.pi/180)/(tstamp)
                        self.nav=data.true_course
                        self.lat=data.latitude
                        self.lon=data.longitude
                        self.client.set('spd', str(data.spd_over_grnd*0.5144))
                        self.client.set('nav', str(data.true_course))
                        self.last=tstamp
            except Exception as e:
                s=("GPS error", repr(e))
    def pos(self):
        return Coordinate( self.lat , self.lon )
    def run(self):
        self.t1 = threading.Thread(target=self.runner_child)
        self.t1.start()
    def stop(self):
        self.t1.stop()
