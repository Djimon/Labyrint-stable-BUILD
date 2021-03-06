﻿using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public class Calculator
    {

        public Calculator()
        {

        }
        
        ///<summary>
        ///gibt die euklidische Distanz zwischen Vekto1 und Vektor2 zurück.
        ///</summary>
        public float getDistance(Vector2f VectorOne, Vector2f VectorTwo)
        {
            float distance;
            return distance = (float)Math.Sqrt(Math.Pow(Math.Abs(VectorOne.X-VectorTwo.X),2) + Math.Pow(Math.Abs(VectorOne.Y-VectorTwo.Y),2));
        }
        
        ///<summary>
        /// Gibt Winkel (in Grad) zwishcen x-Achse und position-Verktor zurück.
        ///</summary>
        public float getWinkel(Vector2f position) //zur x-Achse
        {
            float x = position.X;
            float y = position.Y;

            //////////// MATH.ATAN2////////////////
            /* math.atan2(y,x) gibt Winkel zwischen x-Achse und Vektor(x,y) in Bogenmaß aus.
             * Beachte: beim benutzen von Atan, erst die Y, dann die X koordinate  
             *  (*180/ PI) = Umrechung von Bogenmaß in Grad */
            return (float)(Math.Atan2(y, x) * 180 / Math.PI);  // drehugnswinkel (in grad *180/PI) zum zielvector(y-wert,x-wert)
        }


    }
}
