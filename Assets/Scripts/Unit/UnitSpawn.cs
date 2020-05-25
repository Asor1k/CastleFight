using System;
using UnityEngine;
using CastleFight.Config;
using CastleFight.Core.EventsBus;
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
        private float spawnTimer = 0;
        private bool buildingReady = false;

        private void Awake()
        {
            building.OnReady += OnBuildingReadyHandler;
        }

        private void Start()
        {
            updateManager = ManagerHolder.I.GetManager<IUpdateManager>();
            SubscribeEventHandlers();
        }

        private void OnDestroy()
        {
           UnsubscribeEventHandlers();
        }

        private void SubscribeEventHandlers()
        {
            updateManager.OnUpdate += UpdateHandler;
        }

        private void OnBuildingReadyHandler()
        {
            spawnDelay = building.Config.Delay;
            spawnTimer = spawnDelay;
            buildingReady = true;
            building.OnReady -= OnBuildingReadyHandler;
        }

        private void UnsubscribeEventHandlers()
        {
            updateManager.OnUpdate -= UpdateHandler;
        }

        private void UpdateHandler()
        {
            if(!buildingReady){return;}

            UpdateTimer();

            if(spawnTimer < 0 && !building.SpawnBlocked)
            {
                spawnTimer = spawnDelay;
                SpawnUnit(building.SpawnPoint.position);
            }
        }

        private void UpdateTimer()
        {
            if(spawnTimer >= 0)
                spawnTimer -= Time.deltaTime;
        }

        private Unit SpawnUnit(Vector3 spawnPoint)
        {
            var unit = building.Config.Unit.Create();
            unit.transform.position = spawnPoint;

            EventBusController.I.Bus.Publish(new UnitSpawnedEvent(unit));

            return unit;
        }
    }
}