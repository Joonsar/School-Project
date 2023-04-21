﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Project
{
    public class GameStats
    {
        public List<Entity> EnemiesKilled { get; set; }
        public List<Entity> ItemsCollected { get; set; }

        public int DamageDealt { get; set; }

        public int DamageTaken { get; set; }

        public GameStats()
        {
            EnemiesKilled = new List<Entity>();
            ItemsCollected = new List<Entity>();
            DamageDealt = 0;
            DamageTaken = 0;
        }
    }
}