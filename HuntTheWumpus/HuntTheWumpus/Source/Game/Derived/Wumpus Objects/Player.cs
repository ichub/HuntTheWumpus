using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.Source
{
    class Player
    {
        public int pos = 0;
        public int hp = 0;
        public int arm = 0;
        public Player(int difficulty)
        {
            pos = 0;
            hp = 20;
            Inventory inv = new Inventory(difficulty);
        }

        public int Move(int direction)
        {
            if (/*MAP VERIFICATION*/ true)
            {
                pos = 0;//move up on map;
            }
            return pos;
        }
    }
}


    
