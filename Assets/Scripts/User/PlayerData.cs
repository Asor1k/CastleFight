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
        public int OpeningIndex;
        public int BookSecondsToOpen = 0;
        public long Ticks;
        public bool isNotFirst;

        public PlayerData()
        {
            isNotFirst = true;
            Wins = 0;
            Loses = 0;
            Rating = 0;
            OpeningIndex = -1;
            for(int i = 0; i < 6; i++) //Deafault unitkinds count. TODO: Remove the magic number
            {
                Weights.Add(256); //Deafault max weight. TODO: Remove the magic number
                TalantLevels.Add(0);
            }
        }
    }
}