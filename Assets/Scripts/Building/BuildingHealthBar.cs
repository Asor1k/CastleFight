using System;
using System.Collections;
using System.Collections.Generic;
using Castlefight;
using UnityEngine;

namespace CastleFight
{
    public class BuildingHealthBar : HealthBar
    {
        [SerializeField] private BuildingStats stats;
        
        private void Start()
        {
            stats = GetComponentInParent<BuildingStats>();
            stats.OnDamaged += OnBuildingDamaged;
            stats.OnInit += OnInit;
        }

        private void OnInit()
        {
            UpdateBar();
        }

        private void OnBuildingDamaged()
        {
            UpdateBar();
        }

        private void UpdateBar()
        {
            SetBarValue((float)stats.Hp / stats.MaxHp);
        }

        private void Update()
        {
            var cameraPosition = Camera.main.transform.position;
            var barPosition = transform.position;
                       
            transform.LookAt(new Vector3(cameraPosition.x, cameraPosition.y, barPosition.z));
        }
    }
}
