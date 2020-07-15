
import serial
from time import sleep

class ActuadoresAgropilot:
    def __init__(self):
        self.serial = serial.Serial('/dev/ttyUSB0', 115200, timeout=1)
        self.centro=325

    def crear_giro(self,step):
        self.serial.flush()
        step=(step*-1)+self.centro
        if step<self.centro+100 and step>self.centro-100:
            self.serial.write((str(step)+"+").encode("ascii"))
        print("GIRO CREADO",step,self.centro)
    def reset(self):
        self.serial.write(("*").encode("ascii"))
    def leer_giro(self):
        self.serial.flush()
        self.serial.flushInput()
        sleep(.2)
        self.serial.readline()
        line = self.serial.readline().decode('utf-8').rstrip()
        return int(line)
