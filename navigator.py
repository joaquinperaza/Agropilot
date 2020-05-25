from coordinates import Coordinate
import utils
import time
from simple_pid import PID
from pymemcache.client import base

class Tractor:
    def __init__(self):
        self.client = base.Client(('localhost', 11211))
        self.pid = PID(float(self.client.get("p")), float(self.client.get("i")), float(self.client.get("d")), setpoint=0)
<<<<<<< HEAD
        self.pid.output_limits=(-3000, 3000)
=======
        self.pid.output_limits=(-4000, 4000)
>>>>>>> 24d55039c139d8889a9b9d7bdc200fb14f5fb23f
        self.pid.sample_time=1
        self.steps=0

    def doblar(self,delta,angulo):
        direccion=self.pid(delta)
        dir=1 if direccion>0 else 0
        return (abs(direccion) , dir)
