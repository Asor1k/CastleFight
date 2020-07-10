using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
namespace CastleFight.Core
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Image image;
        private UnitSpawnController unitSpawnController;
        public void Start()
        {
            unitSpawnController = ManagerHolder.I.GetManager<UnitSpawnController>();
            if (gameObject.layer == (int)Team.Team2)
            {
                image.enabled = false;
                this.enabled = false;
            }
        }
        public void Update()
        {
            image.fillAmount = unitSpawnController.SpawnTimer / unitSpawnController.TimerConfig.SpawnTime;
        }
    }
}
