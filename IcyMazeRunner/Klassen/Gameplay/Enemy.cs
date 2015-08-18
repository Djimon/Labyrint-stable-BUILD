using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Gameplay
{
    class Enemy
    {
        Vector2f position;
        Sprite sprite;
        Map mMap;
        float F_speed = 0.5f;

        public Enemy(Vector2f _position, string texturePath, Map map)
        {
            position = _position;
            mMap = map;
            Texture txEnemy = new Texture(texturePath);
            sprite = new Sprite(txEnemy);
        }

        public void move(Map mmmap,GameTime time) //andere Bewegungsmuster (e.g. Pathfinder)
        {
            F_speed = 0.4f * time.ElapsedTime.Milliseconds;
            if (mmmap.iswalkable((sprite), new Vector2f(-F_speed,0)))
            {
                // your code here
            }
        }

        public void move(Vector2f PlayerPosition)  //direkter weg zum player
        {
            Vector2f direction = PlayerPosition - position;
            float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
            position += direction / (length * 5);
        }

        public void update(GameTime Gtime)
        {
            move(this.mMap, Gtime);
        }

        public void draw(RenderWindow win)
        {
            sprite.Position = position;
            win.Draw(sprite);
        }
    }
}
