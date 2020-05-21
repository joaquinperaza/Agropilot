from coordinates import Coordinate
import utils
import time, threading
from vpython import vec
from random import randrange
from simple_pid import PID



class Tractor:
	location=Coordinate(x=0,y=0)
	bearing=0
	m_p_s=0
	pid = PID(.7, .2, .02, setpoint=0)
	
	pid.output_limits=(-20, 20)
	direccion=0
	def move(self):
		while True:
			self.bearing+=self.direccion*-0.1
			l=utils.offset(self.location,self.bearing,self.m_p_s/10)
			self.location=l
			time.sleep(.01)
			

	def run(self):
		t1 = threading.Thread(target=self.move)
		t1.start()
	
	def get_vec(self,z):
		return vec(self.location.x,z,self.location.y)
	
	def doblar(self,v):
		self.direccion=self.pid(v)
		print("PID: ",self.direccion)


	def juego_en_la_direccion(self):
		self.bearing+=randrange(-10,10)
