from coordinates import Coordinate
import utils
import time
from simple_pid import PID
from pymemcache.client import base

class Tractor:
    def __init__(self):
        self.client = base.Client(('localhost', 11211))
        self.pid = PID(float(self.client.get("p")), float(self.client.get("i")), float(self.client.get("d")), setpoint=0)
        self.pid.output_limits=(-90, 90)
        self.pid.sample_time=.5
        self.steps=0
        self.i=float(self.client.get("i"))

    def doblar(self,delta):
        if delta>0.5 or delta<-0.5:
            self.pid.Ki = 0
        else:
            self.pid.Ki=self.i
        direccion=self.pid(delta)
        return int(direccion)

