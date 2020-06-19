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
            stats.OnDamaged += UpdateHealthBar;
            stats.OnReset += UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            SetBarValue((float)stats.Hp / stats.MaxHp);
        }
        
        public void Update()
        {
            var cameraPosition = Camera.main.transform.position;
            var barPosition = transform.position;
                       
            transform.LookAt(new Vector3(cameraPosition.x, cameraPosition.y, barPosition.z));
        }
    }
}