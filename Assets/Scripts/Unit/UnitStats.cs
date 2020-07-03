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
        public int Damage {get{return damage;}}
        public float FractionCritChance{get{return critChance/100f;}} 
        public float CritMultiplier {get{return critMultiplier;}}

        private int hp;
        private int maxHp;
        private float speed;
        private int damage;
        private float critChance;
        private float critMultiplier;

        public void TakeDamage(int damage)
        {
            hp -= damage;
            Debug.Log(damage);
            OnDamaged?.Invoke();
        }
        public void InitCritSkill(float critChance,float critMultiplier)
        {
            this.critChance = critChance;
            this.critMultiplier = critMultiplier;
        }
        public void InitHpSkill(int deltaMaxHp)
        {
            this.maxHp += deltaMaxHp;
            hp = maxHp;
        }
        public void InitSpeedSkill(float speed)
        {
            this.speed = speed;
        }
        public void InitDamageSkill(int deltaDamage)
        {
            this.damage += deltaDamage;
        }
        public void Init(int maxHp, float speed,int damage)
        {
            this.maxHp = maxHp;
            hp = maxHp;
            this.speed = speed;
            this.damage = damage;
        }

        public void Reset()
        {
            hp = maxHp;
            OnReset?.Invoke();
        }
    }
}