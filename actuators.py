import RPi.GPIO as GPIO
import time
from pymemcache.client import base

client = base.Client(('localhost', 11211))

class ActuadoresAgropilot:
	acelerador = 22
	kill = 23 # adapt to your wiring
	fPWM = 50  # Hz (not higher with software PWM)
	a = 10
	b = 2

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

	def setAcelerador(direction):
		duty = a / 180 * direction + b
		acelerador_pwm.ChangeDutyCycle(duty)


	def kill(self):
		if client.get('corte')==0:
			duty = a / 180 * 180 + b
			acelerador_pwm.ChangeDutyCycle(duty)
		else:
			duty = a / 180 * 0 + b
			acelerador_pwm.ChangeDutyCycle(duty)

		

