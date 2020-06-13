using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class UnitAnimationController : MonoBehaviour
    {
        public event Action OnAnimationAttackCallback;


        [SerializeField]
        private Unit unit;
        [SerializeField]
        private Animator animator;
        private Action attackCallback;

        void Start()
        {
            unit.OnKilled += OnUnitKilled;
            unit.OnReset += OnUnitReset;
        }

        
        public void AnimAttackCallback()
        {
            OnAnimationAttackCallback?.Invoke();
        }

        void Update()
        {
            UpdateSpeed(unit.CurrentSpeed);
        }

        public void Attack(Action callback)
        {
            attackCallback = callback;
            animator.SetTrigger("Attack");
        }

        public void AttackAnimCallback()
        {
            if(attackCallback != null)
            {
                attackCallback.Invoke();
                attackCallback = null;
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
