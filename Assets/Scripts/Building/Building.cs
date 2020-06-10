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

        [SerializeField]
        private Transform spawnPoint;
        [SerializeField] private BuildingBehavior behavior;
        private BaseBuildingConfig config;
        private bool spawnBlocked = false;

        
        public void Init(BaseBuildingConfig config)
        {
            this.config = config;
        } 

        public void Build()
        {
            //TODO: Implement building construction
            OnReady?.Invoke();
        }
    }
}