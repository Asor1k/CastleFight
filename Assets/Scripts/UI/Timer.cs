using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
namespace CastleFight.Core
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform timerVfxTransform;
        [SerializeField] private RectTransform timerTransform;
        [SerializeField] private float distance;
        [SerializeField] private Vector2 endPosition;
        private float maxTime;
        private Vector2 beginPosition;
        private UnitSpawnController unitSpawnController;
        private bool hasVf = false;
        public void Start()
        {
            unitSpawnController = ManagerHolder.I.GetManager<UnitSpawnController>();
            maxTime = unitSpawnController.TimerConfig.SpawnTime;
            if (gameObject.layer == (int)Team.Team2)
            {
                image.enabled = false;
                this.enabled = false;
            }
            if (timerVfxTransform == null) return;
            else
            {
                hasVf = true;
                beginPosition = timerVfxTransform.anchoredPosition;
                endPosition = -timerTransform.sizeDelta;
                distance = Mathf.Abs(beginPosition.x - endPosition.x);
            }
        }
        
        public void Update()
        {
            float fraction = 1-(unitSpawnController.SpawnTimer / maxTime);
            image.fillAmount = fraction;
            if (hasVf)
            {
                timerVfxTransform.anchoredPosition = new Vector2(beginPosition.x - (1 - fraction) * distance, 0);
            }
        }
    }
}
