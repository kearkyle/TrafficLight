//****************************************************************************************************************************
//Program name: "Traffic Light".  This program is a simple simulation of a traffic light.                                                           *
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

//Purpose:  This program will display a working traffic light

//Files in project: trafficinterface.cs, trafficmain.cs, build.sh

//This file's name: trafficicinterface.cs
//This file purpose: This file contains the structures of the user interface window
//Date last modified: 2020-October-10
//To compile trafficinterface.cs:
//              mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:trafficinterface.dll trafficinterface.cs

﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;

public class Traffic : Form
{
  
    private Label author = new Label();
    private Button startbutton = new Button();
    private Button stopbutton = new Button();
    private Button speedbutton = new Button();
    private Button exitbutton = new Button();
    private Panel headerpanel = new Panel();
    private Panel controlpanel = new Panel();
    private Size maximuminterfacesize = new Size(330, 700);
    private Size minimuminterfacesize = new Size(330, 700);
    private enum Light {White,Red,Green,Yellow};  
    private static System.Timers.Timer timecount = new System.Timers.Timer();  
    public Traffic()  //Constructor
    {//Set the size of the user interface box.
        MaximumSize = maximuminterfacesize;
        MinimumSize = minimuminterfacesize;
        //Initialize text strings
        Text = "Traffic Light";
        author.Text = "Traffic Light by Vong Chen";
        startbutton.Text = "Start";
        stopbutton.Text = "Stop";
        speedbutton.Text = "Fast";
        exitbutton.Text = "Exit";

        //Set sizes
        Size = new Size(330, 700);
        author.Size = new Size(300, 40);
        startbutton.Size = new Size(50, 50);
        stopbutton.Size = new Size(50, 50);
        speedbutton.Size = new Size(50, 50);
        exitbutton.Size = new Size(50, 50);
        headerpanel.Size = new Size(400, 100);
        controlpanel.Size = new Size(400, 100);

        //Set colors
        headerpanel.BackColor = Color.Blue;
        controlpanel.BackColor = Color.DarkBlue;
        startbutton.BackColor = Color.Cornsilk;
        speedbutton.BackColor = Color.Cornsilk;
        stopbutton.BackColor = Color.Cornsilk;
        exitbutton.BackColor = Color.Red;

        //Set fonts
        author.Font = new Font("Times New Roman", 12, FontStyle.Regular);
        startbutton.Font = new Font("Liberation Serif", 10, FontStyle.Regular);
        stopbutton.Font = new Font("Liberation Serif", 10, FontStyle.Regular);
        speedbutton.Font = new Font("Liberation Serif", 10, FontStyle.Regular);
        exitbutton.Font = new Font("Liberation Serif", 10, FontStyle.Regular);        

        //Set locations
        headerpanel.Location = new Point(0, 0);
        author.Location = new Point(75, 40);
        startbutton.Location = new Point(17, 12);
        speedbutton.Location = new Point(97, 12);
        stopbutton.Location = new Point(177, 12);
        exitbutton.Location = new Point(257, 12);
        headerpanel.Location = new Point(0, 0);
        controlpanel.Location = new Point(0, 600);

        //Associate the Compute button with the Enter key of the keyboard
        AcceptButton = startbutton;

        //Add controls to the form
        Controls.Add(headerpanel);
        headerpanel.Controls.Add(author);
        Controls.Add(controlpanel);
        controlpanel.Controls.Add(startbutton);
        controlpanel.Controls.Add(speedbutton);
        controlpanel.Controls.Add(stopbutton);
        controlpanel.Controls.Add(exitbutton);
        //time
        timecount.Enabled = false;
        //Register the event handler.  In this case each button has an event handler, but no other 
        //controls have event handlers.
        startbutton.Click += new EventHandler(trafficstart);
        speedbutton.Click += new EventHandler(trafficspeed);
        stopbutton.Click += new EventHandler(trafficstop);
        exitbutton.Click += new EventHandler(stoprun);  //The '+' is required.
        //Open this user interface window in the center of the display.
        CenterToScreen();
        

    }//End of constructor Traffic
    
    Light current = Light.White;
    //OnPaint function which draws the circles
    protected override void OnPaint(PaintEventArgs ee)
    {   Graphics graph = ee.Graphics;
        graph.FillRectangle(Brushes.Silver,0,100,330,500);
        switch(current)
        {case Light.White:
                    graph.FillEllipse(Brushes.Gray,115,150,100,100);
                    graph.FillEllipse(Brushes.Gray,115,300,100,100);
                    graph.FillEllipse(Brushes.Gray,115,450,100,100);
                break;
         case Light.Red:
                    graph.FillEllipse(Brushes.Red,115,150,100,100);
                    graph.FillEllipse(Brushes.Gray,115,300,100,100);
                    graph.FillEllipse(Brushes.Gray,115,450,100,100);
                break;
         case Light.Green:
                    graph.FillEllipse(Brushes.Gray,115,150,100,100);
                    graph.FillEllipse(Brushes.Gray,115,300,100,100);
                    graph.FillEllipse(Brushes.Green,115,450,100,100);
                break;
         case Light.Yellow:
                    graph.FillEllipse(Brushes.Gray,115,150,100,100);
                    graph.FillEllipse(Brushes.Yellow,115,300,100,100);
                    graph.FillEllipse(Brushes.Gray,115,450,100,100);
                break;
        }
        base.OnPaint(ee);
    }    
    int speed = 1;
    //Method to calculate the time for changing lights when Lights are under operation
    protected void traffic(System.Object sender, ElapsedEventArgs evt)
    {
        switch(current){
            case Light.Red:   timecount.Interval = (int)(6000/speed);
                              current = Light.Green;
               break;
            case Light.Green: timecount.Interval = (int)(2000/speed);
                              current = Light.Yellow;
               break;
            case Light.Yellow: timecount.Interval = (int)(8000/speed);
                              current = Light.Red;                       
               break;            
        }
        Invalidate();
    }//


    //Method to execute when the start button receives an event, namely: receives a mouse click
    protected void trafficstart(Object sender, EventArgs events) 
    {
        System.Console.WriteLine("Traffic light in operation. Default speed is slow.");
        current = Light.Red;
        timecount.Elapsed += new ElapsedEventHandler(traffic);
        timecount.Interval = (int)8000;
        timecount.Enabled = true;
        Invalidate();
    }//End of cleartext 

    //Methos to execute when the speedbutton receives an event
    protected void trafficspeed(Object sender, EventArgs events)
    {
        if (speedbutton.Text == "Fast"){
            System.Console.WriteLine("Traffic Light is changing in fast mode");
            speed = 2;
            speedbutton.Text = "Slow";}   
        else {
            System.Console.WriteLine("Traffic Light is in slow mode");
            speed = 1;
            speedbutton.Text = "Fast";}
        Invalidate();
    }

    //Method to execute when the stopbutton receives an event
    protected void trafficstop(Object sender, EventArgs events)
    {
        if (stopbutton.Text == "Stop"){
            System.Console.WriteLine("Traffic Light is paused");
            timecount.Enabled = false;
            stopbutton.Text = "Resume";}
        else {
            System.Console.WriteLine("Traffic light is resuming operation");
            timecount.Enabled = true;
            stopbutton.Text = "Stop";}
    }
    //Method to execute when the exit button receives an event, namely: receives a mouse click
    protected void stoprun(Object sender, EventArgs events)
    {
        Close();
    }//End of stoprun
    
}//End of clas Fibuserinterface
