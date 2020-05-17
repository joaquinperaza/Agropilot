import io

import pynmea2
import threading
from pymemcache.client import base
import socket
from db import DB
import time

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
		self.listen.connect(self.net.get_ip(),8888)
		self.client = base.Client(('localhost', 11211))
	def runner_child(self):
		while True:
			if 1==1:
				lines = self.listen.readlines()
				for line in lines:
					if (line.startswith("$GNGGA")):
						data = pynmea2.parse(line)
						self.client.set('lat',str(data.latitude))
						self.client.set('lon', str(data.longitude))
						self.client.set('sat', str(data.num_sats))
						self.client.set('age', str(data.age_gps_data))

					if (line.startswith("$GNRMC")):
						data = pynmea2.parse(line)
						self.client.set('lat', str(data.latitude))
						self.client.set('lon', str(data.longitude))
						self.client.set('spd', str(data.spd_over_grnd))
						self.client.set('nav', str(data.true_course))

	def run(self):
		t1 = threading.Thread(target=self.runner_child)
		t1.start()
	def stop(self):
		t1.stop()
