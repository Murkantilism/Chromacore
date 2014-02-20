# Chromacore
# Purpose: To calculate the exact X postion of Notes based on timestamps
# @author: Deniz Ozkaynak

#Import listdir
from os import listdir
from os.path import isfile, join

def readTimestamps():
	# Open or create the timestamp text file
	myTimestamps = open("level5_timestamps.txt", "w")
	
	mypath = "F:\Classes\Chromacore\FL Studio Work\happ piano mau5\Level5_pickupTracks"
	myFiles = listdir(mypath)
	#onlyFiles = [ f for f in listdir(mypath) if isfile(join(mypath,f)) ]
	
	for file in myFiles:
		file = file.replace(".mp3", "")
		myTimestamps.write(str(file) + "\n")
	
	
	myTimestamps.close()

readTimestamps()