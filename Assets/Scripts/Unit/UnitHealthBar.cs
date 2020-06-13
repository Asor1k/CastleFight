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
            transform.LookAt(Camera.main.transform);
        }
    }
}