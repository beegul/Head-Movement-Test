import BNO055
#import msvcrt
#import os.path
#from datetime import datetime
#import time

bno = BNO055.BNO055(serial_port='COM3')

#start_time = time.time()
#filename = 'Participant'
#i = 1

#while os.path.exists('C:/filepath/%s%s.csv' % (filename, i)):	#checks to see if there is a log file with the same name.
	#i = i + 1

#file = open('C:/filepath/%s%s.csv' % (filename, i) "w") #create new log file with the unique name.
#file.write("Start Time:, %s" %datetime.now() + ",,,,,,Duration\n")

bno.begin()
print('Sensor Connection Succesful')

while True:
        heading, roll, pitch = bno.read_euler()
        sys, gyro, accel, mag = bno.get_calibration_status()
        print('Heading =' + str(heading) + ',' + 'Roll =' + str(roll) + ',' + 'Pitch =' + str(pitch) + ',' + 'Sys_cal =' + str(sys) + ',' + 'Gyro_cal =' + str(gyro) + ',' + 'Accel_cal =' + str(accel) + ',' + 'Mag_cal =' + str(mag))
		
		#print('Testing')
				
		#file.write("Heading={0:0.2F}, Roll={1:0.2F}, Pitch={2:0.2F},\tSys_cal={3}, Gyro_cal={4}, Accel_cal={5}, Mag_cal={6}, {7}".format(heading, roll, pitch, sys, gyro, accel, mag, time.time() - startime) + "\n")
		#if msvcrt.kbhit():
			#if ord(msvcrt.getch()) == 27:
				#file.close()
 
