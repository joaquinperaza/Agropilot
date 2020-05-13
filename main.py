from actuators import ActuadoresAgropilot
from gps import GPSData

control=ActuadoresAgropilot()
control.setup()
gps=GPSData()
gps.run()
