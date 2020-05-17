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
        data = self.sock.recv(1024)
        print(data)


class GPSData:
	global t1, ser, sio, client, listen, db
	net=new DB()
	listen = TCPConnection()
	listen.connect(net.get_ip(),8888)
	client = base.Client(('localhost', 11211))
	def runner_child(self):
		while True:
			try:
				line = listen.readlines()	
				# if (line.startswith("$GNGGA")):
				# 	data = pynmea2.parse(line)
				# 	client.set('lat', data.latitude)
				# 	client.set('lon', data.longitude)
				# 	client.set('sat', data.num_sats)
				# 	client.set('age', data.age_gps_data)
				# if (line.startswith("$GNRMC")):
				# 	data = pynmea2.parse(line)
				# 	client.set('lat', data.latitude)
				# 	client.set('lon', data.longitude)
				# 	client.set('spd', data.spd_over_grnd)
				# 	client.set('nav', data.true_course)
			except Exception as e:
				print('Device error: {}'.format(e))
				time.sleep(5)
	def run(self):
		t1 = threading.Thread(target=self.runner_child)
		t1.start()
	def stop(self):
		t1.stop()