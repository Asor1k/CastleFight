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
        [SerializeField] private float speed;
        private float maxTime;
        private Vector2 beginPos;
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
                beginPos = timerVfxTransform.anchoredPosition;
                speed = timerVfxTransform.sizeDelta.x / maxTime;
            }
        }
        
        public void Update()
        {
            image.fillAmount = unitSpawnController.SpawnTimer / maxTime;
            if (hasVf)
            {
                timerVfxTransform.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
                if (Mathf.Abs(timerVfxTransform.anchoredPosition.x) >= timerTransform.sizeDelta.x-beginPos.x)
                {
                    timerVfxTransform.anchoredPosition = beginPos;
                }
            }
        }
    }
}
