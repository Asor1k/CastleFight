using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Castlefight
{
    public class BuildingStats : MonoBehaviour
    {
        public event Action OnDamaged;
        public event Action OnInit;
        
        public int Hp
        {
            get => hp;
        }

        public int MaxHp
        {
            get => maxHp;
        }
        private int hp;
        private int maxHp;

        public void Init(int maxHp)
        {
            this.maxHp = maxHp;
            hp = maxHp;
            
            OnInit?.Invoke();
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
            OnDamaged?.Invoke();
        }

        public void Reset()
        {
            hp = maxHp;
        }
    }
}