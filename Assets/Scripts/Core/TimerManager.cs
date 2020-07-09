using System.Collections;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
namespace CastleFight.Core {
    public class TimerManager : MonoBehaviour
    {
        public float SpawnTimer => spawnTimer;

        [SerializeField] private TimerConfig timerConfig;
        private float spawnTimer;

        public void Start()
        {
            EventBusController.I.Bus.Subscribe<GameSetReadyEvent>(OnGameStart);
            ManagerHolder.I.AddManager(this);
        }
        public void OnGameStart(GameSetReadyEvent gameSetReadyEvent)
        {
            Init();
        }
        public void Init()
        {
            spawnTimer = timerConfig.SpawnTime;
        }
        public void Update()
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                spawnTimer = timerConfig.SpawnTime;
                EventBusController.I.Bus.Publish(new SpawnUnitsEvent());
            }
        }
        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<GameSetReadyEvent>(OnGameStart);
        }

    }
}