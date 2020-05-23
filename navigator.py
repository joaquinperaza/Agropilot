from coordinates import Coordinate
import utils
import time, threading
from vpython import vec
from random import randrange
from simple_pid import PID
from pymemcache.client import base

class Tractor:
    def __init__(self):
        self.client = base.Client(('localhost', 11211))
        self.pid = PID(float(self.client.get("p")), float(self.client.get("i")), float(self.client.get("d")), setpoint=0)
        self.pid.output_limits=(-4000, 34000)
        self.pid.sample_time=1
        self.steps=0

    def doblar(self,delta,angulo):
        direccion=self.pid(delta)
        dir=1 if direccion>0 else 0
        return (abs(direccion) , dir)
