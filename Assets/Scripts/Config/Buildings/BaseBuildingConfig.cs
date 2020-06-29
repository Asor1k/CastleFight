using CastleFight.Config;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace CastleFight.Config
{
    public abstract class BaseBuildingConfig : ScriptableObject
    {
        public Sprite Icon => icon;
        public BaseUnitConfig Unit => unit;
        public string Name => buildingName;
        public int Cost => cost;
        public List<BuildingLevelStats> LevelsStats => levelsStats;
        
        [SerializeField]
        private List<BuildingLevelStats> levelsStats;
        [SerializeField]
        protected Building prefab;
        [SerializeField]
        protected BaseUnitConfig unit;
        [SerializeField]
        protected Sprite icon;
        [SerializeField]
        protected string buildingName;
        [SerializeField]
        protected int cost;


        public virtual Building Create()
        {
            var building = Instantiate(prefab);
            building.Init(this);

            return building;
        }
    }

    [Serializable]
    public struct BuildingLevelStats
    {
        public int MaxHp;
        public int GoldIncome;
        public int GoldDelay;
        public int Delay;
        public int Cost;
    }
}