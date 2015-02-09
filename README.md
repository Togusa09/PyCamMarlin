# PyCamMarlin 
Converts PyCam output to marlin compatible GCode

Allows you to combine multiple multiple GCode files from PyCam and process them together so that they can be 
executed by a marlin based CNC machine, which in my case is a FabTotum.

## Known Issues ##
- Can't remove added files
- Can't sort added files
- Speed is not detected from source file and harcoded to 10000RPM

PyCam can be found at http://pycam.sourceforge.net/
