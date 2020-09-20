using System;
using System.Collections;
using System.Collections.Generic;
using CastleFight.Config;
using UnityEngine;

namespace CastleFight
{
    public class UnitAnimationController : MonoBehaviour
    {
        public event Action OnAttackAnimationHit;
        public event Action OnAttackAnimationEnd;

        [SerializeField]
        private Unit unit;
        [SerializeField]
        private Animator animator;
        private Action attackCallback;
        private Action attackEndCallback;
        private BaseUnitConfig unitConfig;
        private Stat attackSpeed;

        void Start()
        {
            unit.OnKilled += OnUnitKilled;
            unit.OnReset += OnUnitReset;
            attackSpeed = (Stat)unit.Stats.GetStat(StatType.AttackSpeed);
            animator.SetFloat("AttackSpeed", attackSpeed.Value);
        }
        
        public void AnimAttackCallback()
        {
            OnAttackAnimationHit?.Invoke();
        }

        void Update()
        {
            UpdateSpeed(unit.CurrentSpeed);
        }

        public void Attack(Action attackCallback, Action attackEndCallback)
        {
            this.attackCallback = attackCallback;
            this.attackEndCallback = attackEndCallback;
            animator.SetTrigger("Attack");
        }

        public void AttackAnimCallback()
        {
            if(attackCallback != null)
            {
                if (!unit.Alive) return;
                attackCallback.Invoke();
                attackCallback = null;
            }
        }

        public void AttackAnimEndCallback()
        {
            if (attackEndCallback != null)
            {
                if (!unit.Alive) return;
                attackEndCallback.Invoke();
                attackEndCallback = null;
            }
        }

        private void UpdateSpeed(float speed)
        {
            animator.SetFloat("Speed", speed);
        }

        private void OnUnitKilled()
        {
            animator.SetTrigger("Kill");
        }

        private void OnUnitReset()
        {
            animator.SetTrigger("Reset");
        }

    }
}
