import RPi.GPIO as GPIO
import time
from pymemcache.client import base
from time import sleep


class ActuadoresAgropilot:
	client = base.Client(('localhost', 11211))
	acelerador = 22
	kill = 23
	fPWM = 50  # Hz (not higher with software PWM)
	a = 10
	b = 2
	DIR = 20   # Direction GPIO Pin
	STEP = 21  # Step GPIO Pin
	CW = 1     # Clockwise Rotation
	CCW = 0    # Counterclockwise Rotation
	SPR = 1600
	DELAY = .00150

	def setup():
		global acelerador_pwm, kill_pwm
		GPIO.setmode(GPIO.BOARD)
		GPIO.setup(acelerador, GPIO.OUT)
		acelerador_pwm = GPIO.PWM(acelerador, fPWM)
		acelerador_pwm.start(0)
		GPIO.setup(kill, GPIO.OUT)
		kill_pwm = GPIO.PWM(kill, fPWM)
		kill_pwm.start(0)
		client.set('acelerador', 0)
		client.set('corte', 0)
		t1 = threading.Thread(target=self.runner_child)
		t1.start()
		t2 = threading.Thread(target=self.runner_child2)
		t2.start()
		GPIO.setup(STEP, GPIO.OUT)
		GPIO.setup(DIR, GPIO.OUT)

	def setAcelerador(direction):
		duty = a / 180 * direction + b
		acelerador_pwm.ChangeDutyCycle(duty)


	def kill(self):
		if client.get('corte')==0:
			duty = a / 180 * 0 + b
			acelerador_pwm.ChangeDutyCycle(duty)
		elif client.get('corte')==1:
			duty = a / 180 * 180 + b
			acelerador_pwm.ChangeDutyCycle(duty)

	def runner_child(self):
		while True:
			try:
				self.kill()
				if client.get('acelerador')!=-1:
					self.setAcelerador(client.get('acelerador'))
					client.set('acelerador',-1)
			except Exception as e:
				print repr(e)

	def runner_child2(self):
		while True:
			if client.get('dir')!=-1 and client.get('step')!=-1:
				GPIO.output(DIR, client.get('dir'))
				for x in range(client.get('step')):
					GPIO.output(STEP, GPIO.HIGH)
					sleep(delay)
					GPIO.output(STEP, GPIO.LOW)
					sleep(delay)
				client.set('dir',-1)
				client.set('step',-1)

		

