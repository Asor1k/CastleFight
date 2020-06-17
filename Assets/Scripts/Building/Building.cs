using System;
using Core;
using UnityEngine;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;

namespace CastleFight
{
    public class Building : MonoBehaviour
    {
        public event Action OnReady;
        public BuildingBehavior Behavior => behavior;
        public BaseBuildingConfig Config => config;
        public bool SpawnBlocked => spawnBlocked;
        public Transform SpawnPoint => spawnPoint;
        public int Lvl => lvl;

        [SerializeField]
        private Transform spawnPoint;
        [SerializeField] private BuildingBehavior behavior;
        private BaseBuildingConfig config;
        private int lvl;
        private bool spawnBlocked = false;

        
        public void Init(BaseBuildingConfig config)
        {
            this.config = config;
            lvl = 1; //TODO: delete the magic number
        }
        public void UpgradeBuilding()
        {

            if (behavior.goldManager.IsEnough(behavior))
            {
                behavior.goldManager.MakeGoldChange(-config.Cost);
                lvl++; //Upgrade
            }
        }
        public void Build()
        {
            //TODO: Implement building construction
            OnReady?.Invoke();
        }
    }
}