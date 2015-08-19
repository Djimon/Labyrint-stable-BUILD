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

            //Vektor- Position des Players
            Vector2f playerDistance = new Vector2f(player.getXPosition(),player.getYPosition());

            //Vektoren ausgehend vom Enemy nach oben unten rechts und links -> Distanz zum Player
            Vector2f tmp = new Vector2f(position.X, position.Y-100);
            if (mmmap.iswalkable((sprite), new Vector2f(position.X, position.Y))) { }
            else
            {
                float testTop = calc.getDistance(playerDistance, tmp);

                tmp = new Vector2f(position.X + 100, position.Y);
                float testRight = calc.getDistance(playerDistance, tmp);

                tmp = new Vector2f(position.X, position.Y + 100);
                float testBot = calc.getDistance(playerDistance, tmp);

                tmp = new Vector2f(position.X - 100, position.Y);
                float testLeft = calc.getDistance(playerDistance, tmp);

                //Zuweisung der Distanzen und (0,1,2,3) kodierter Richtung
                enemy_move top = new enemy_move(0, testTop);
                enemy_move right = new enemy_move(1, testRight);
                enemy_move bot = new enemy_move(2, testBot);
                enemy_move left = new enemy_move(3, testLeft);

                enemy_move[] choose = new enemy_move[4];
                choose[0] = top;
                choose[1] = right;
                choose[2] = bot;
                choose[3] = left;

                //Insertionsort auf das enemy_move Array über die Distanzen
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

                int go_tmp, go;
                go = 4;

                go_tmp = choose[3].direction;
                switch (go_tmp)
                {
                    case 0: if (mMap.iswalkable((sprite), new Vector2f(0, -F_speed))) { go = 0; } break;
                    case 1: if (mMap.iswalkable((sprite), new Vector2f(F_speed, 0))) { go = 1; } break;
                    case 2: if (mMap.iswalkable((sprite), new Vector2f(0, F_speed))) { go = 2; } break;
                    case 3: if (mMap.iswalkable((sprite), new Vector2f(-F_speed, 0))) { go = 3; } break;
                }

                go_tmp = choose[2].direction;
                switch (go_tmp)
                {
                    case 0: if (mMap.iswalkable((sprite), new Vector2f(0, -F_speed))) { go = 0; } break;
                    case 1: if (mMap.iswalkable((sprite), new Vector2f(F_speed, 0))) { go = 1; } break;
                    case 2: if (mMap.iswalkable((sprite), new Vector2f(0, F_speed))) { go = 2; } break;
                    case 3: if (mMap.iswalkable((sprite), new Vector2f(-F_speed, 0))) { go = 3; } break;
                }

                go_tmp = choose[1].direction;
                switch (go_tmp)
                {
                    case 0: if (mMap.iswalkable((sprite), new Vector2f(0, -F_speed))) { go = 0; } break;
                    case 1: if (mMap.iswalkable((sprite), new Vector2f(F_speed, 0))) { go = 1; } break;
                    case 2: if (mMap.iswalkable((sprite), new Vector2f(0, F_speed))) { go = 2; } break;
                    case 3: if (mMap.iswalkable((sprite), new Vector2f(-F_speed, 0))) { go = 3; } break;
                }

                go_tmp = choose[0].direction;
                switch (go_tmp)
                {
                    case 0: if (mMap.iswalkable((sprite), new Vector2f(0, -F_speed))) { go = 0; } break;
                    case 1: if (mMap.iswalkable((sprite), new Vector2f(F_speed, 0))) { go = 1; } break;
                    case 2: if (mMap.iswalkable((sprite), new Vector2f(0, F_speed))) { go = 2; } break;
                    case 3: if (mMap.iswalkable((sprite), new Vector2f(-F_speed, 0))) { go = 3; } break;
                }

                switch (go)
                {
                    case 0: position = new Vector2f(position.X, position.Y - 1); break;
                    case 1: position = new Vector2f(position.X + 1, position.Y); break;
                    case 2: position = new Vector2f(position.X, position.Y + 1); break;
                    case 3: position = new Vector2f(position.X - 1, position.Y); break;
                    case 4: break;
                }
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
