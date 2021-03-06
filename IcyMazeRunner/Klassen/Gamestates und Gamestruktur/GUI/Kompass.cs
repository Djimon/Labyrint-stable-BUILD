﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Gamestates_und_Gamestruktur.GUI
{
    class Kompass //Kompass
    {
       
        
        Sprite spNeedle;
        Sprite spnew;
        Vector2f vCompass; //Kompassmittelpunkt
        Vector2f vTarget; // Zielobjekt
        View view;

        // Konstruktor
        public Kompass(Vector2f midpoint, View view, Vector2f target)
        {
            this.spNeedle = new Sprite(new Texture("Texturen/Menü+Anzeigen/GUI/needle.png"));
            this.view = view;
            this.vCompass = midpoint;
            this.vTarget = target;
        }

        public Vector2f getVector(Vector2f a, Vector2f b)
        {
            return (b - a);  // nicht sicher, ob der richtig berechnet wird
        }

        public float getWinkel(Vector2f position)
        {
            float x = position.X;
            float y = position.Y;
            return (float)(Math.Atan2(y, x) * 180 / Math.PI);  // drehugnswinkel (in grad *180/PI) zum zielvector(y-wert,x-wert)
        }


        // grafik rotieren via Winkel      
        // name="oldBitmap"  >Bitmap grafik
        // name="angle"  >winkel in Gradmaß

        //private static Bitmap RotateImageByAngle(System.Drawing.Image oldBitmap, float angle)
        //{
        //    var newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height);
        //    newBitmap.SetResolution(oldBitmap.HorizontalResolution, oldBitmap.VerticalResolution);
        //    var graphics = Graphics.FromImage(newBitmap);
        //    graphics.TranslateTransform((float)oldBitmap.Width / 2, (float)oldBitmap.Height / 2);
        //    graphics.RotateTransform(angle);
        //    graphics.TranslateTransform(-(float)oldBitmap.Width / 2, -(float)oldBitmap.Height / 2);
        //    graphics.DrawImage(oldBitmap, new Point(0, 0));
        //    return newBitmap;
        //}

        static Sprite RotateImageByAngle(Sprite spPic, float angle)
        {
            //Sprite spOldPic = spPic;
            Sprite spNewPic = new Sprite(spPic);
            spNewPic.Rotation = angle;
            return spNewPic;
        }


        public void update(Vector2f target)
        {
            vTarget = target;
            if (getWinkel(getVector(vCompass, vTarget)) != 0)
            {
                spnew = RotateImageByAngle(spNeedle, getWinkel(getVector(vCompass, vTarget)));
            }
            else spnew = spNeedle;
        }


        public void draw(RenderWindow win)
        {
            // work on a copy, instead of the original, for the original could be reused outside this scope
            

            // modify sprite, to fit it in the gui
            float viewScale = (float)view.Size.X / win.Size.X;

            spnew.Scale *= viewScale;
            spnew.Position = view.Center - view.Size / 2F + spnew.Position * viewScale;

            // draw the sprite
            win.Draw(spnew);
        }


    }
}
