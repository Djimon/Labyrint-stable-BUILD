using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Gameplay
{
    public class enemy_move
    {
        public int direction;
        public float distance;

        public enemy_move(int direction, float distance)
        {
            this.direction = direction;
            this.distance = distance;
        }
    }
}
