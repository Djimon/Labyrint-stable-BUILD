using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyMazeRunner.Klassen;

namespace IcyMazeRunner.Klassen.Menüs
{
    class Inventory
    {
        View vInvView;

        /* ~~~~ Variablen für Menüsteuerung ~~~~*/
        int select;
        int I_State = 0;
        bool isPressed;
        bool closeInv;

        public void setCloseInv(bool value)
        {
            closeInv = value;
        }
        public bool getCloseMenu()
        {
            return closeInv;
        }
         /* ~~~~ Konstruktor ~~~~*/
        public Inventory(Player Runner)
        {
            // Initialisieren der Steuerungsvariablen
            select = 0;
            isPressed = false;
            closeInv = false;
            vInvView = new View(new FloatRect(0,0,1062,72));

            float scaleX = 0.9f;
            float scaleY = 0.9f;
            float xCoord = Runner.getXPosition() - 595;
            float yCoord = Runner.getYPosition() - 360;

        }

        public EGameStates update()
        {
            /*~~Menüsteuerung~~*/

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
            {
                select = (select + 1) % 4; // mit Laden und Speichern auf 5 erhöhen
                isPressed = true;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
            {
                select = (select - 1) % 4; // mit Laden und Speichern auf 5 erhöhen
                isPressed = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
                isPressed = false;

            // Inventarzustände
            // Cases anpassen!
            switch (select)
            {
                case 0: //slot1
                    {
                        //spSelected.Texture = txContinueSelected;
                        break;
                    }
                case 1: //slot2
                    {
                        //spSelected.Texture = txGoMainMenuSelected;
                        break;
                    }
                case -3:
                    {
                        //spSelected.Texture = txGoMainMenuSelected;
                        break;
                    }
                case 2: //slot3
                    {
                        //spSelected.Texture = txControlsSelected;
                        break;
                    }
                case -2:
                    {
                        //spSelected.Texture = txControlsSelected;
                        
                        break;
                    }
                case 3: //slot4
                    {
                        //spSelected.Texture = txLoadGameSelected;
                        break;
                    }
                case -1:
                    {
                        //spSelected.Texture = txLoadGameSelected;
                        break;
                    }
            }


            // Update der Gamestates
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                setCloseInv(true);
            // mehrere Reiter: 
            // Reiter 1: Inventar(I)
            // Reiter 2: Rätsel (R)
            // Reiter 3: Diary (D)
            
            if (Keyboard.IsKeyPressed(Keyboard.Key.I))
                I_State = 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.R))
                I_State = 2;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                I_State = 3;
            if (Keyboard.IsKeyPressed(Keyboard.Key.C))
                I_State = 0;
            
            
            switch(I_State)
            {
                case 0: //Charackterübersicht aktiv
                        //Reiter = Charackterfenster.png
                        break;
                case 1: //Map Übericht - Fortschritt
                        //Reiter = Inventory.png
                        break;
                case 2: //Rätsel-Inventar aktiv
                        //Reiter = inventroy.png
                        break;
                case 3: // Tagebuch /Questlog aktiv
                        //Reiter = questlog.png
                        break;
            }
            
            
            if ((select == 1 || select == -3) && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                vInvView.Reset(new FloatRect(0, 0, 1062, 720));  //globale Fenstervariable
                return EGameStates.mainMenu;
            }
            if ((select == 2 || select == -2) && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                vInvView.Reset(new FloatRect(0, 0, 1062, 720));  //globale Fenstervariable
                return EGameStates.controls;
            }
            if ((select == 3 || select == -1) && Keyboard.IsKeyPressed(Keyboard.Key.Return))  //Platzhalter Map auswahl
            {
                vInvView.Reset(new FloatRect(0, 0, 1062, 720));  //globale Fenstervariable
                return EGameStates.mainMenu;
            }

            return EGameStates.inGame;
        }

         public void draw(RenderWindow window)
        {
            //window.Draw(spMenuBackground);
            //window.Draw(spMenuHeader);
            //window.Draw(spSelected);
            window.SetMouseCursorVisible(true);
           
        }

    }
}
