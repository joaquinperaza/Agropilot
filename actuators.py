
import serial


class ActuadoresAgropilot:
    def __init__(self):
        self.serial = serial.Serial('/dev/ttyUSB0', 115200, timeout=1)


    def crear_giro(self,step,direccion):
        self.serial.flush()
        stepdir='+'
        if int(direccion)==1:
            stepdir='-'
        steps=str(int(step))
        self.serial.write((steps+stepdir).encode("ascii"))
        print("GIRO CREADO")
