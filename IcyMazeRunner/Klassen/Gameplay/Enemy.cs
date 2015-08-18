using SFML.Graphics;
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
            // 0- top
            // 1- right
            // 2- bot
            // 3- left
            enemy_move top = new enemy_move(0, _distance());
            enemy_move right = new enemy_move(1, _distance());
            enemy_move bot = new enemy_move(2, _distance());
            enemy_move left = new enemy_move(3, _distance());

            enemy_move[] choose = new enemy_move[4];
            choose[0] = top;
            choose[1] = right;
            choose[2] = bot;
            choose[3] = left;
            
        }

        public void move(Vector2f PlayerPosition)  //direkter weg zum player
        {
            Vector2f direction = PlayerPosition - position;
            float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
            position += direction / (length * 5);
        }

        public override void update(GameTime gametime)
        {
            return;
        }

        //public override void draw(RenderWindow win)
        //{
        //    sprite.Position = position;
        //    win.Draw(sprite);
        //}
    }
}
