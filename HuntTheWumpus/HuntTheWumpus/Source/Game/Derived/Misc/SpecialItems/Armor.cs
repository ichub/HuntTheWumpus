﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus
{
    class Armor
    {
        public int ArmorID;

        public Armor(int itemID)
        {
            this.ArmorID = itemID;
        }

        private int ProtectionStrength()
        {
            if (this.ArmorID == 3)
            {
                return 2;
            }
            else if (this.ArmorID == 4)
            {
                return 4;
            }
            else if (this.ArmorID == 5)
            {
                return 3;
            }
            else if (this.ArmorID == 6)
            {
                return 3;
            }
            else if (this.ArmorID == 7)
            {
                return 5;
            }
            else if (this.ArmorID == 8)
            {
                return 4;
            }
            else
            {
                return 1;
            }
        }

        public double CalculateDamage(int damage)
        {
            return damage / this.ProtectionStrength();
        }
    }
}
