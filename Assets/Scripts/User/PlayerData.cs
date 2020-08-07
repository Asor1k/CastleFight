using System.Collections;
using System;
using CastleFight.Config;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class PlayerData
    {
        public int Wins;
        public int Loses;
        public int Rating;
        
        public List<int> Weights = new List<int>();
        public List<int> TalantLevels = new List<int>();

        public int[] CardsTimeToOpen = new int[4];
        public int openingIndex;
        public long ticks;
        public PlayerData()
        {
            Wins = 0;
            Loses = 0;
            Rating = 0;
            openingIndex = -1;
            for(int i = 0; i < 6; i++) //Deafalt unitkinds count. TODO: Remove the magic number
            {
                Weights.Add(256); //Deafalt max weight. TODO: Remove the magic number
                TalantLevels.Add(0);
            }
        }
    }
}