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

        public Sprite Frame => frame;
        public string Name => buildingName;
        public int Cost => cost;
        public BuildingUpgradeNode LevelUgradeTree => levelUpgradesTree;
        public int SumForSale => sumForSale;
        public int GetMaxLevels ()
        {
            int n = 1;
            BuildingUpgradeNode upgradeNode = levelUpgradesTree;
            while (true)
            {
                if (upgradeNode.Nodes.Count == 0) return n;
                n++;
                upgradeNode = upgradeNode.Nodes[0];
            }
        } 

        [SerializeField]
        private BuildingUpgradeNode levelUpgradesTree;
        [SerializeField]
        protected Building prefab;
        [SerializeField]
        protected Sprite icon;
        [SerializeField]
        protected Sprite frame;
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
        public Sprite Icon;
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