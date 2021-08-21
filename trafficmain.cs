//****************************************************************************************************************************
//Program name: "Traffic Light".  This is a simple simulation of a working traffic light.                                                           *
//Copyright (C) 2020  Vong Chen                                                                                        *
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License  *
//version 3 as published by the Free Software Foundation.                                                                    *
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied         *
//warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.     *
//A copy of the GNU General Public License v3 is available here:  <https://www.gnu.org/licenses/>.                           *
//****************************************************************************************************************************

//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**

//Author: Vong Chen
//Mail: vchen7@csu.fullerton.edu

//Program name:  Traffic Light
//Programming language: C Sharp
//Date development of program began: 2020-October-13
//Date of last update: 2020-October-14
//Course ID: CPSC 223N-01
//Assignment number: 02
//Date assignment is due: 2020-October-14

//Purpose:  This program will display a working Traffic light

//Files in project: trafficinterface.cs, trafficmain.cs, build.sh

//This file's name: trafficmain.cs
//This file purpose: This file will run the user interface
//Date last modified: 2020-October-10

//To compile trafficicinterface.cs:
//                              mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:trafficinterface.dll trafficinterface.cs
//To compile trafficmain.cs
//                              mcs -r:System -r:System.Windows.Forms -r:trafficinterface.dll -out:Traffic.exe trafficmain.cs


using System;

using System.Windows.Forms;

public class Trafficmain
{
    public static void Main()
    {System.Console.WriteLine("Welcome to Light traffic program");
     Traffic trafficapp = new Traffic();
     Application.Run(trafficapp);
     System.Console.WriteLine("Main mnethod will now shutdown");
    }
}
