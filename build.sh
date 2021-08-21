echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo Compile trafficinterface.cs to create the file: trafficinterface.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll  -out:trafficinterface.dll trafficinterface.cs

echo Compile trafficmain.cs and link the previously created dll files to create an executable file. 
mcs -r:System -r:System.Windows.Forms -r:trafficinterface.dll -out:Traffic.exe trafficmain.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 1 program.
./Traffic.exe

echo The script has terminated.
