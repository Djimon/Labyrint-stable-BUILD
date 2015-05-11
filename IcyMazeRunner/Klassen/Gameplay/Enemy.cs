﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Gameplay
{
    class Enemy : GameObject
    {
        Vector2f position;
        Sprite sprite;

        public Enemy(Vector2f _position, string texturePath)
        {
            position = _position;

            Texture txEnemy = new Texture(texturePath);
            sprite = new Sprite(txEnemy);
        }

        public void move() //andere Bewegungsmuster (e.g. Pathfinder)
        {
        
        }

        public void move(Vector2f PlayerPosition)  //direkter weg zum player
        {
            Vector2f direction = PlayerPosition - position;
            float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
            position += direction / (length * 5);
        }


        public void draw(RenderWindow win)
        {
            sprite.Position = position;
            win.Draw(sprite);
        }
    }
}
