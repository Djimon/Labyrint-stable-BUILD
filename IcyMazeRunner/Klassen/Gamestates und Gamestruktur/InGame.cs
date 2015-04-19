﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyMazeRunner.Klassen;
using SFML.Audio;

namespace IcyMazeRunner.Klassen
{
    class InGame : GameStates
    {
        /* ~~~~~~~~ VARIABLEN UND KONSTANTEN ~~~~~~~~*/

        int level = 0;
        GameTime time = new GameTime();

        Map map;
        View view;

        Player Runner;

        Sprite backGround;
        Sprite Fog_of_War;

        private int typeOfDeath;
        /*
         * 0 = alive
         * 1 = Death by falling
         * 2 = instant Death by trap
         * 3 = Death by damage
        */
        public bool isDeathAnimationOver;




        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        Sprite ziel;  // hier map/ blocktype=4 (blue= ziel) ??

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/


        /*~~~~~~~~~~~~~    Getter and Setter    ~~~~~~~~~~~~~~~*/

        public int getTypeOfDeath()
        {
            return typeOfDeath;
        }

        public void setTypeOfDeath(int data)
        {
            typeOfDeath = data;
        }


        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        
        
        /* ~~~~ Initialisierung des Spiels ~~~~ */
        public void initialize()
        {
           

            time = new GameTime();
            time.start();
            view = new View(new FloatRect(0, 0, 1062, 720));
            
            backGround = new Sprite(new Texture("Texturen/Map/background.png"));
            backGround.Position = new Vector2f(0, 0);

            Fog_of_War = new Sprite(new Texture("Texturen/Map/Fog_of_War.png"));
            Fog_of_War.Position = new Vector2f(-5, -5);

            setTypeOfDeath(0);
            isDeathAnimationOver=false;
            
          
        }

        /* ~~~~ Inhalte laden ~~~~ */
        public void loadContent(){
          
            /*
            Map-Positionen:
            Map_tutorial (190,0)
            Map_2 (2263, 3336)
             */



            // Levelzuweisung

            switch (level)
            {
                case 0:
                    map = new Map(new Bitmap("Texturen/Map/Map_1.bmp"));
                    Runner = new Player(new Vector2f(910, 100), map);

                    break;

                case 1:
                    map = new Map(new Bitmap("Texturen/Map_1.bmp"));
                    Runner = new Player(new Vector2f(0, 0), map);
                    break;
                    
                case 2:
                    map = new Map(new Bitmap("Texturen/Map_2.bmp"));
                    Runner = new Player(new Vector2f(2263,3336), map);
                    break;

            }

          
            //        hier Fallen und Hindernisse laden???
            //         ziel?
           

            
        }


        /* ~~~~ Update ~~~~ */
        public EGameStates update(GameTime gametime)
        {

            if (get_Gap_Collision(Runner))
            {
                Runner.setPlayerHealth(0);
                setTypeOfDeath(1);
            }

            time.update();

            if (Runner.getPlayerHealth() == 0)
            {
                
                Runner.DeathAnimation(getTypeOfDeath());


                if (Runner.DeathWatch.Watch.ElapsedMilliseconds > 4000)
                {
                    level--;
                    return EGameStates.gameOver;
                }
            }

            if (level != level) // Kollision mit Treppe= true
            {
                level++;
                Program.game.handleGameState();
                return EGameStates.inGame;
            }

            if (level == 15)
            {
                view = new View(new FloatRect(0, 0, 1062, 720));
                return EGameStates.gameWon;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.O))
            {
                view=new View(new FloatRect(0, 0, 1062, 720));
                return EGameStates.gameWon;
            }

            Runner.update(time);
            // bewegliche Mauern (if-Abfrage), Kollision mit Schalter
            // später: Bewegung der Gegner, Geschosse, Anzeigen, Kollision

            backGround.Position = new Vector2f(view.Center.X - 531, view.Center.Y - 360);
            Fog_of_War.Position = new Vector2f(view.Center.X - 531, view.Center.Y - 360);
            view.Move(new Vector2f((Runner.getXPosition() + (Runner.getWidth() / 2)), (Runner.getYPosition() + (Runner.getHeigth() / 2))) - view.Center);




            /*~~~~~~~Collision mit Ziel, SPrite ziel muss noch übergebenw erden aus (Map/Blocks?)~~~~*/

            //if (collision(Runner.getplayerSprite, (float)Runner.getWidth(), (float)Runner.getHeigth(), ziel.getPosition(), (float)ziel.getWidth(), (float)ziel.getHeight()))
            //{
            //    Console.Write("Collision!!1elf");
            //    view = new View(new FloatRect(0, 0, 1062, 720));
            //    return EGameStates.gameWon;
            //}


            return EGameStates.inGame;
        }

        public static bool collision(Vector2f sprite1, float high1, float width1, Vector2f sprite2, float high2, float width2)
        {
            Vector2f Mobj1 = new Vector2f(sprite1.X + width1 / 2, sprite1.Y + high1 / 2);
            Vector2f Mobj2 = new Vector2f(sprite2.X + width2 / 2, sprite2.Y + high2 / 2);

            float wmid1 = width1 / 2;
            float wmid2 = width2 / 2;

            float hmid1 = high1 / 2;
            float hmid2 = high2 / 2;

            float betrag_x = Math.Abs(Mobj1.X - Mobj2.X);
            float betrag_y = Math.Abs(Mobj1.Y - Mobj2.Y);

            if (betrag_x < wmid1 + wmid2 && betrag_y < hmid1 + hmid2)
                return true;

            else
                return false;
        }

        public bool get_Gap_Collision(Player player)
        {
            // Kachel an Spielerposition mit Farbe der Bitmap und damit Kachelfarbe des Lochblocks vergleichen
            if (map.GetPixel(((player.getXPosition() + (player.getWidth() / 2)) / getBlocksize) + 1, ((player.getYPosition() + (player.getHeigth()).getBlocksize) + 1).Name.equals(orange)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      
        public void draw(RenderWindow win)
        {
            win.Draw(backGround);
            win.SetView(view);
            map.draw(win);
            Runner.draw(win);
            win.Draw(Fog_of_War);
        }
        
    }
}