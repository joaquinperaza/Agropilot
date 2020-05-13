import io

import pynmea2
import serial
import threading

ser = serial.Serial('/dev/ttyS1', 38400, timeout=50.0)
sio = io.TextIOWrapper(io.BufferedRWPair(ser, ser))


class GPSData:
	lat=0.0
	lon=0.0
	heading=0.0
	speed=0.0
	t1=None
	def runner_child(self):
		while True:
			try:
				line = sio.readline()
				if (line.startswith("$GPGGA")):
					data = pynmea2.parse(line)
				if (line.startswith("$GPRMC")):
					data = pynmea2.parse(line)
			except serial.SerialException as e:
				print('Device error: {}'.format(e))
				break
			except pynmea2.ParseError as e:
				print('Parse error: {}'.format(e))
				continue
	def run(self):
		t1 = threading.Thread(target=self.runner_child)
		t1.start()
	def stop(self):
		t1.stop()