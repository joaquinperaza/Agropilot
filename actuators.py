import RPi.GPIO as GPIO
import time

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
		acelerador_pwm = GPIO.PWM(kill, fPWM)
		acelerador_pwm.start(0)

	def setAcelerador(direction):
		duty = a / 180 * direction + b
		acelerador_pwm.ChangeDutyCycle(duty)


	def kill(self):
		duty = a / 180 * 180 + b
		acelerador_pwm.ChangeDutyCycle(duty)
	

