# Chromacore
# Purpose: To calculate the exact X postion of Notes based on timestamps
# @author: Deniz Ozkaynak

#Import listdir
from os import listdir
from os.path import isfile, join
import os, sys

def readTimestamps():
	# Open or create the timestamp text file
	myTimestamps = open("level10_timestamps.txt", "w")
	
	mypath = "F:\\Classes\\Chromacore\\FL Studio Work\\berlin_4\\Level10_pickupTracks"
	myFiles = listdir(mypath)
	#onlyFiles = [ f for f in listdir(mypath) if isfile(join(mypath,f)) ]
	
	for file in myFiles:
		# Remove file extensions
		file = file.replace(".mp3", "")
		
		# Replace hyphens with decimals
		file = file.replace("-", ".")
		
		
		# If the timestamp is at zero mins, remove the first
		# two characters Ex: 0.13.490 becomes 13.490
		if (file[0] == "0"):
			file = file[2:]
		
		# If the timestamp is at 1 min, remove the first
		# two characters & add 60 to the next two digits
		# Ex: 1.13.490 becomes 73.490
		elif (file[0] == "1"):
			file = file[2:]
			file = str(int(str(int(file[0])) + str(int(file[1]))) + 60) + file[-4:]
		
		# If the timestamp is at 2 mins, remove the first
		# two characters & add 120 to the next two digits
		# Ex: 2.13.490 becomes 133.490
		elif (file[0] == "2"):
			file = file[2:]
			file = str(int(str(int(file[0])) + str(int(file[1]))) + 120) + file[-4:]
		
		# If the timestamp is at 3 mins, remove the first
		# two characters & add 180 to the next two digits
		elif (file[0] == "3"):
			file = file[2:]
			file = str(int(str(int(file[0])) + str(int(file[1]))) + 180) + file[-4:]
			
		# If the timestamp is at 4 mins, remove the first
		# two characters & add 240 to the next two digits
		elif (file[0] == "4"):
			file = file[2:]
			file = str(int(str(int(file[0])) + str(int(file[1]))) + 240) + file[-4:]
			
		# If the timestamp is at 5 mins, remove the first
		# two characters & add 300 to the next two digits
		elif (file[0] == "5"):
			file = file[2:]
			file = str(int(str(int(file[0])) + str(int(file[1]))) + 300) + file[-4:]
			
		# If the timestamp is at 6 mins, remove the first
		# two characters & add 360 to the next two digits
		elif (file[0] == "6"):
			file = file[2:]
			file = str(int(str(int(file[0])) + str(int(file[1]))) + 360) + file[-4:]
			
		# If the timestamp is at 7 mins, remove the first
		# two characters & add 420 to the next two digits
		elif (file[0] == "7"):
			file = file[2:]
			file = str(int(str(int(file[0])) + str(int(file[1]))) + 420) + file[-4:]
			
		# If the timestamp is at 8 mins, remove the first
		# two characters & add 480 to the next two digits
		elif (file[0] == "8"):
			file = file[2:]
			file = str(int(str(int(file[0])) + str(int(file[1]))) + 480) + file[-4:]
			
		# Write the timestamps to the text file
		myTimestamps.write(str(file) + "\n")
	
	# Close the text file
	myTimestamps.close()

readTimestamps()