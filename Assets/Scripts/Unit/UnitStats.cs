using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class UnitStats : MonoBehaviour
    {
        public event Action OnDamaged;
        public event Action OnReset;

        public int Hp{get{return hp;}}
        public int MaxHp{get{return maxHp;}}
        public float Speed {get{return speed;}}

        private int hp;
        private int maxHp;
        private float speed;

        public void TakeDamage(int damage)
        {
            hp -= damage;
            OnDamaged?.Invoke();
        }

        public void Init(int maxHp, float speed)
        {
            this.maxHp = maxHp;
            hp = maxHp;
            this.speed = speed;
        }

        public void Reset()
        {
            hp = maxHp;
            OnReset?.Invoke();
        }
    }
}