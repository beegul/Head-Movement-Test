import BNO055
import msvcrt#allows key input
import os.path#allows the checking of file pathnames
from datetime import datetime#allows the use of "datetime". using "from datetime" allows you to use datetime.now instead of datetime.datetime.now()
import time

bno = BNO055.BNO055(serial_port='COM3')

try:
	bno.begin()
except RuntimeError:
	print('Failed to open sensor')
	time.sleep(0.1)
	
print('START')

startime = time.time()#gets the start time of the program.
#filename = "participant"
run = True
#i = 1

#while os.path.exists("C:/Users/Jack/Desktop/%s%s.csv" %(filename, i)):#checks to see if a file with the same name exists, if it does increment the i value by one.#
#	i += 1
#
#file = open("C:/Users/Jack/Desktop/%s%s.csv" %(filename, i), "w")#create a file with the name "participant" and add the icrement "i" value to make different files each time.
#
#file.write("Start Time:, %s" %datetime.now() + ",,,,,,Duration\n")#writes the start time and date to the file.

while run:
	try:
		heading, roll, pitch = bno.read_euler()
		sys, gyro, accel, mag = bno.get_calibration_status()
		print("Heading={0:0.2F} Roll={1:0.2F} Pitch={2:0.2F}\tSys_cal={3} Gyro_cal={4} Accel_cal={5} Mag_cal={6}".format(heading, roll, pitch, sys, gyro, accel, mag)) #prints data to the console     
		#time.sleep(1)
	except RuntimeError:
		print('Failed to read sensor')
		time.sleep(0.1)
#    file.write("Heading={0:0.2F}, Roll={1:0.2F}, Pitch={2:0.2F},\tSys_cal={3}, Gyro_cal={4}, Accel_cal={5}, Mag_cal={6}, {7}".format(heading, roll, pitch, sys, gyro, accel, mag, time.time() - startime) + "\n")#writes data to the file
#    if msvcrt.kbhit():#returns true if a keypress is waiting to be read
#	    if ord(msvcrt.getch()) == 27:#"ord" return the unicode character of a single keypress, "getch" reads a keypress and returns the resulting character
#		    run = False#turns run to false to stop the loop.
#		    file.close()#closes the file and saves all of the written data to it



print('EXIT')

