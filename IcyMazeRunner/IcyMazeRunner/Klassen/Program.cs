﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner
{
    class Program
    {

        //Attribute hier:

        static Player Runner;
        static void Main(string[] args)
        {
            RenderWindow Window = new RenderWindow(new VideoMode(1280, 720), "IcyMazeRunner");

            Initialize();

            while (Window.IsOpen())
            {
                update();

            }
            
        }

       static void Initialize(){
           Runner = new Player();
           // Map
           // später: Gegner, Fallen(Geschosse), Anzeige (Timer/Stoppuhr), HP-Balken

       }


       static void update()
       {
           Runner.move();
           //Sichtkreis, bewegliche Mauern (if-Abfrage), Kollision mit Schalter
           // später: Bewegung der Gegner, Geschosse, Anzeigen, Kollision
       }

       static void Windowclosed(Object sender, EventArgs e)
       {
           ((RenderWindow)sender).Close();
       }
    }
}