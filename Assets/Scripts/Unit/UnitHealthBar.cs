using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class UnitHealthBar : HealthBar
    {
        [SerializeField]
        private UnitStats stats;

        void Start()
        {
            stats.OnHpChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar(Stat hpStat)
        {
            SetBarValue((float)hpStat.Value / (float)hpStat.MaxValue);
        }
        
        public void Update()
        {
            var cameraPosition = Camera.main.transform.position;
            var barPosition = transform.position;
                       
            transform.LookAt(new Vector3(cameraPosition.x, cameraPosition.y, barPosition.z));
        }
    }
}