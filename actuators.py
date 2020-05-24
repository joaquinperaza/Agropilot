
import serial


class ActuadoresAgropilot:
    def __init__(self):
        self.serial = serial.Serial('/dev/ttyACM0', 115200, timeout=1)


    def crear_giro(self,step,direccion):
        self.ser.flush()
        stepdir='+'
        if direccion==1:
            stepdir='-'
        steps=str(int(step))
        ser.write(steps+stepdir)
        print("GIRO CREADO")
