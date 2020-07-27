﻿using System.Collections;
using System.Collections.Generic;
using Castlefight;
using UnityEngine;

namespace CastleFight
{
    public class DamageableBuilding : MonoBehaviour, IDamageable
    {
        public bool Alive
        {
            get { return building.IsStanding; }
        }

        public TargetType Type => TargetType.Building;
        public Vector3 ViewOffset => Vector3.zero;

        public Transform Transform
        {
            get { return transform; }
        }

        [SerializeField] private BuildingStats stats;
        [SerializeField] private Building building;
        
        public void TakeDamage(float damage)
        {
            stats.TakeDamage(damage);
        }
    }
}
