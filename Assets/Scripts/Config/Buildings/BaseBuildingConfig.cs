using CastleFight.Config;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace CastleFight.Config
{
    public abstract class BaseBuildingConfig : ScriptableObject
    {
        public Sprite Icon => icon;
        public string Name => buildingName;
        public int Cost => cost;
        public BuildingUpgradeNode LevelUgradeTree => levelUpgradesTree;
        public int SumForSale => sumForSale;

        [SerializeField]
        private BuildingUpgradeNode levelUpgradesTree;
        [SerializeField]
        protected Building prefab;
        [SerializeField]
        protected Sprite icon;
        [SerializeField]
        protected string buildingName;
        [SerializeField]
        protected int cost;
        [SerializeField]
        protected int sumForSale;


        public virtual Building Create()
        {
            var building = Instantiate(prefab);
            building.Init(this);

            return building;
        }
    }

    [Serializable]
    public struct BuildingLevelConfig
    {
        public BaseUnitConfig Unit;
        public int Level;
        public int MaxHp;
        public int GoldIncome;
        public int GoldDelay;
        public int Delay;
        public int Cost;
        public int SumForSale;
        public int GoldPerSecond;
    }

    [Serializable]
    public struct BuildingUpgradeNode
    {
        public BuildingLevelConfig Config;
        public List<BuildingUpgradeNode> Nodes;
    }
}