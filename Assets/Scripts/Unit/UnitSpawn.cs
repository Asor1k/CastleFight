using System;
using UnityEngine;
using CastleFight.Config;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using Core;
using CastleFight.Core;

namespace CastleFight
{
    public class UnitSpawn : MonoBehaviour
    {
        [SerializeField]
        private Building building;
        private IUpdateManager updateManager;
        private float spawnDelay;
        [SerializeField] private float spawnTimer = 0;
        private bool buildingReady = false;

        private void Awake()
        {
            building.OnReady += OnBuildingReadyHandler;
            EventBusController.I.Bus.Subscribe<SpawnUnitsEvent>(OnUnitsSpawn);
        }

        private void OnBuildingReadyHandler()
        {
            spawnDelay = building.Config.Levels[building.Lvl - 1].Delay;
            spawnTimer = spawnDelay;
            buildingReady = true;
            building.OnReady -= OnBuildingReadyHandler;
        }

        private void OnUnitsSpawn(SpawnUnitsEvent spawnUnitsEvent)
        {
            if (building.SpawnBlocked || !building.Behavior.IsPlaced) return;
            SpawnUnit(building.SpawnPoint.position, building.Behavior.Team);
        }
        private void Update()
        {
            /* if(!buildingReady){return;}

             UpdateTimer();

             if(spawnTimer < 0 && !building.SpawnBlocked)
             {
                 spawnTimer = spawnDelay;
                 SpawnUnit(building.SpawnPoint.position, building.Behavior.Team);
             }*/
        }
        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<SpawnUnitsEvent>(OnUnitsSpawn);
        }

        private void UpdateTimer()
        {
            if (spawnTimer >= 0)
                spawnTimer -= Time.deltaTime;
        }

        private Unit SpawnUnit(Vector3 spawnPoint, Team team)
        {
            var unit = building.Config.Levels[building.Lvl - 1].Unit.Create(team);
            unit.transform.position = spawnPoint;

            EventBusController.I.Bus.Publish(new UnitSpawnedEvent(unit));

            return unit;
        }
    }
}