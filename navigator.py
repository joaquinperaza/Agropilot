from coordinates import Coordinate
import utils
import time, threading
from vpython import vec
from random import randrange
from simple_pid import PID

class Tractor:
    def __init__(self):
        self.client = base.Client(('localhost', 11211))
        self.pid = PID(float(self.client.get("p")), float(self.client.get("i")), float(self.client.get("d")), setpoint=0)
        self.pid.output_limits=(-20, 20)
        self.steps=0

    def doblar(self,delta,angulo):
        direccion=self.pid(delta)
        if(direccion!=angulo):
            return direccion*1000
        else:
            return 0
            