using System;
using UnityEngine;
using CastleFight.Config;

namespace CastleFight
{
    public abstract class  Unit : MonoBehaviour
    {
        public event Action OnInit;
        public float CurrentSpeed{get{return agent.Speed;}}
        public virtual float Speed{get{return config.Speed;}}
        public virtual float EnemyDetectRange{get{return config.EnemyDetectRange;}}
        public virtual float AttackDistance{get{return config.AttackDistance;}}

        [SerializeField]
        protected Agent agent; 
        [SerializeField]
        protected UnitAnimationController animationController;
        protected BaseUnitConfig config;
        private Team team;
        private Damageable target;

        public virtual void Init(BaseUnitConfig config, Team team)
        {
            this.team = team;
            gameObject.layer = (int)team;
            this.config = config;
            OnInit?.Invoke();
        }

        public virtual void MoveTo(Vector3 point)
        {
            agent.MoveTo(point);
        }

        public virtual void Stop()
        {
            agent.Stop();
        }
        
        public virtual void Attack(Damageable target)
        {
            animationController.Attack(()=>{target.Hit(1);});
        }
    }
}