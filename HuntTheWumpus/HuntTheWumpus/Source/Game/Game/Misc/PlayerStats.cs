using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HuntTheWumpus.Source
{
    public class PlayerStats
    {
        public string Name { get; set; }

        public int CurrentRoom { get; set; }
        public int HP { get; set; }
        public int Armor { get; set; }
        public int Gold { get; set; }
        public int Score { get; private set; }
        public int FromTrivia { get; set; }
        public int FromGold { get; set; }
        public int FromMisc { get; set; }
        public int AmountOfArrows { get; set; }
        public int CorrectAmountAnswered { get; set; }

        public float MaxSpeed { get; set; }
        public float SpeedDelta { get; set; }

        public Inventory Inventory { get; set; }

        private const int TriviaWeight = 100;
        private const int GoldWeight = 50;
        private const int MiscWeight = 25;

        /// <summary>
        /// Initializes the PlayerStatsclass
        /// </summary>
        /// <param name="name">name of the player</param>
        public PlayerStats(string name)
        {
            this.Name = name;
            this.CurrentRoom = 0;
            this.AmountOfArrows = 3;
            this.HP = 3;
            this.Gold = 0;
            this.Score = 0;
            this.CorrectAmountAnswered = 0;
            this.MaxSpeed = 5;
            this.SpeedDelta = 2;
            this.Inventory = new Inventory(20);
        }

        /// <summary>
        /// Adds trivia weight to the score.
        /// </summary>
        public void AddTriviaScore()
        {
            this.FromTrivia += PlayerStats.TriviaWeight;
            this.Score += PlayerStats.TriviaWeight;
            this.Gold++;
        }

        /// <summary>
        /// Adds gold score to the gamescore
        /// </summary>
        public void AddGoldScore()
        {
            this.FromGold += PlayerStats.GoldWeight;
            this.Score += PlayerStats.GoldWeight;
        }

        /// <summary>
        /// Adds to the score: misc stuff
        /// </summary>
        public void AddMiscScore()
        {
            this.FromMisc += PlayerStats.MiscWeight;
            this.Score += PlayerStats.MiscWeight;
        }

        /// <summary>
        /// Reset the player stats
        /// </summary>
        public void Reset()
        {
            this.CurrentRoom = 0;
            this.HP = 3;
            this.Gold = 100;
            this.Score = 0;
            this.AmountOfArrows = 3;
            this.CorrectAmountAnswered = 0;
            this.MaxSpeed = 5;
            this.SpeedDelta = 2;
            this.Inventory = new Inventory(20);
        }
    }
}



