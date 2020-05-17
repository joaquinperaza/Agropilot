import RPi.GPIO as GPIO
import time
from pymemcache.client import base
from time import sleep
import threading

class ActuadoresAgropilot:
	def __init__(self):
		self.client = base.Client(('localhost', 11211))
		self.acelerador = 22
		self.kill = 23
		self.fPWM = 50  # Hz (not higher with software PWM)
		self.a = 10.0
		self.b = 2.0
		self.DIR = 24  # Direction GPIO Pin
		self.STEP = 16  # Step GPIO Pin
		self.CW = 1     # Clockwise Rotation
		self.CCW = 0    # Counterclockwise Rotation
		self.SPR = 1600
		self.DELAY = .00150

	def setup(self):
		GPIO.setmode(GPIO.BOARD)
		GPIO.setup(self.acelerador, GPIO.OUT)
		self.acelerador_pwm = GPIO.PWM(self.acelerador, self.fPWM)
		self.acelerador_pwm.start(0)
		GPIO.setup(self.kill, GPIO.OUT)
		self.kill_pwm = GPIO.PWM(self.kill, self.fPWM)
		self.kill_pwm.start(0)
		self.client.add('acel', "0")
		self.client.add('corte', "0")
		self.client.add('step',"0")
		self.client.add('dir', "0")
		t1 = threading.Thread(target=self.runner_child)
		t1.start()
		t2 = threading.Thread(target=self.runner_child2)
		t2.start()
		GPIO.setup(self.STEP, GPIO.OUT)
		GPIO.setup(self.DIR, GPIO.OUT)

	def setAcelerador(self,direction):
		duty = self.a / 180.0 * float(direction) + self.b
		self.acelerador_pwm.ChangeDutyCycle(duty)


	def cut(self):
		print("debug",int(self.client.get("corte")))
		if int(self.client.get('corte'))==0:
			duty = self.a / 180.0 * 0.0 + self.b
			self.kill_pwm.ChangeDutyCycle(duty)
		elif int(self.client.get('corte'))==1:
			duty = self.a / 180.0 * 180.0 + self.b
			kill_pwm.ChangeDutyCycle(duty)

	def runner_child(self):
		while True:
			try:
				self.cut()

				if int(self.client.get('acel')) != -1:
					self.setAcelerador(int(self.client.get('acel')))
					self.client.add('acel',"-1")
			except Exception as e:
				print ("Actuator ERR",repr(e))

	def runner_child2(self):
		while True:
			if int(self.client.get('dir'))!=-1 and int(self.client.get('step'))!=-1:
				GPIO.output(self.DIR, int(self.client.get('dir')))
				for x in range(int(self.client.get('step'))):
					GPIO.output(self.STEP, GPIO.HIGH)
					sleep(self.delay)
					GPIO.output(self.STEP, GPIO.LOW)
					sleep(self.delay)
				self.client.add('dir',"-1")
				self.client.add('step',"-1")

		

