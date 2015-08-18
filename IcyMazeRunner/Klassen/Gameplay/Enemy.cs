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
        Player player;
        Vector2f position;
        Sprite sprite;
        Calculator calc = new Calculator();
        Map mMap;
        float F_speed = 0.5f;

        public Enemy(Vector2f _position, string texturePath, Map map, Player player)
        {
            this.player = player;
            position = _position;
            mMap = map;
            Texture txEnemy = new Texture(texturePath);
            sprite = new Sprite(txEnemy);
        }

        public void move(Map mmmap,GameTime time) //andere Bewegungsmuster (e.g. Pathfinder)
        {
            F_speed = 0.4f * time.ElapsedTime.Milliseconds;
 
             //0- top
             //1- right
             //2- bot  
             //3- left
            Vector2f playerDistance = new Vector2f(player.getXPosition(),player.getYPosition());

            Vector2f tmp = new Vector2f(position.X, position.Y+1);
            float testTop = calc.getDistance(playerDistance, tmp);

            tmp = new Vector2f(position.X+1, position.Y );
            float testRight = calc.getDistance(playerDistance, tmp);

            tmp = new Vector2f(position.X, position.Y - 1);
            float testBot = calc.getDistance(playerDistance, tmp);

            tmp = new Vector2f(position.X-1, position.Y);
            float testLeft = calc.getDistance(playerDistance, tmp);

            enemy_move top = new enemy_move(0, testTop);
            enemy_move right = new enemy_move(1, testRight);
            enemy_move bot = new enemy_move(2, testBot);
            enemy_move left = new enemy_move(3, testLeft);
           
            enemy_move[] choose = new enemy_move[4];
            choose[0] = top;
            choose[1] = right;
            choose[2] = bot;
            choose[3] = left;

            //Insertionsort auf das enemy_move array über die Distances
            for (int i = 0; i < choose.Length - 1; i++)
            {
                int j = i + 1;
                while (j > 0)
                {
                    if (choose[j - 1].distance > choose[j].distance)
                    {
                        float temp = choose[j - 1].distance;
                        choose[j - 1].distance = choose[j].distance;
                        choose[j].distance = temp;
                    }
                    j--;
                }
            }

            int go = 0;
            if (mmmap.iswalkable((sprite), new Vector2f(F_speed, 0)))
            {
                go = choose[3].direction;
            }
            if (mmmap.iswalkable((sprite), new Vector2f(F_speed, 0)))
            {
                go = choose[2].direction;
            } 
            if (mmmap.iswalkable((sprite), new Vector2f(F_speed, 0)))
            {
                go = choose[1].direction;
            } 
            if (mmmap.iswalkable((sprite), new Vector2f(F_speed, 0)))
            {
                go = choose[0].direction;
            }
            
             
            

             switch (go)
             {
                 case 0: position = new Vector2f(position.X, position.Y + 1); break;
                 case 1: position = new Vector2f(position.X+1, position.Y ); break;
                 case 2: position = new Vector2f(position.X, position.Y - 1); break;
                 case 3: position = new Vector2f(position.X - 1, position.Y); break;
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
