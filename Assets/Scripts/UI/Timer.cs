using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
namespace CastleFight.Core
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Image image;
        private TimerManager timerManager;
        public void Start()
        {
            timerManager = ManagerHolder.I.GetManager<TimerManager>();
        }
        public void Update()
        {
            image.fillAmount = timerManager.SpawnTimer / timerManager.TimerConfig.SpawnTime;
        }
    }
}
