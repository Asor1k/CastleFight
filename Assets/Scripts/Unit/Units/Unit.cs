using System;
using UnityEngine;
using CastleFight.Config;
using System.Threading.Tasks;

namespace CastleFight
{
    public abstract class  Unit : MonoBehaviour
    {
        public event Action OnInit;
        public event Action OnKilled;
        public event Action OnReset;

        public bool Alive{get{return alive;}}

        public float CurrentSpeed{get{return agent.Speed;}}
        public virtual float Speed{get{return stats.Speed;}}
        public virtual float EnemyDetectRange{get{return config.EnemyDetectRange;}}
        public virtual float AttackDistance{get{return config.AttackDistance;}}
        
        [SerializeField]
        protected Agent agent; 
        [SerializeField]
        protected Collider collider;
        [SerializeField]
        protected UnitAnimationController animationController;
        [SerializeField]
        protected BaseUnitConfig config;
        [SerializeField]
        protected UnitHealthBar healthBar;
        [SerializeField]
        protected BaseUnitSkillConfig skillConfig;

        [SerializeField]
        internal UnitStats stats;

        protected Team team;
        protected IDamageable target;
        protected int hp;
        protected int maxHp;
        protected bool alive = true;
        protected bool readyToAttack = true;
        
        
        public virtual void Init(BaseUnitConfig config, Team team)
        {
            this.team = team;
            gameObject.layer = (int)team;
            this.config = config;
            maxHp = config.MaxHp;
            hp = maxHp;
            alive = true;
            stats.Init(config.MaxHp, config.Speed, config.Damage);
            stats.OnDamaged += OnUnitDamaged;
            if (skillConfig != null)
            skillConfig.InitEffects(this);
            OnInit?.Invoke();
        }

        public virtual void MoveTo(Vector3 point)
        {
            if(!alive) return;

            agent.MoveTo(point);
        }

        public virtual void Stop()
        {
            if(!alive) return;
            
            agent.Stop();
        }
        
        public virtual void Attack(IDamageable target)
        {
            if(!target.Alive || !alive || !readyToAttack) return;
            
            readyToAttack = false;
            StartAttackCooldown();
            
            agent.LookAt(target.Transform);
            animationController.Attack(()=>{target.TakeDamage(Damage());});
        }
        private int Damage()
        { 
            int damage = stats.Damage;
            if (stats.FractionCritChance == 0f) return damage;
            if (UnityEngine.Random.value <= stats.FractionCritChance)
            {
                return Mathf.RoundToInt(damage * stats.CritMultiplier);
            }
            else return damage;
        }
        
        private void Kill()
        {
            alive = false;
            collider.enabled = false;
            agent.Disable();
            healthBar.Show(false);
            OnKilled?.Invoke();
            DelayDestroy();
        }

        private async Task DelayDestroy()
        {
            await Task.Delay(3000);
            Destroy(gameObject);
        }
        
        public void Reset()
        {
            collider.enabled = true;
            stats.Reset();
            agent.Enable();
            OnReset?.Invoke();
        }

        private void OnUnitDamaged()
        {
            if(stats.Hp <= 0)
            {
                Kill();
            }
        }

        private async Task StartAttackCooldown()
        {
            var miliseconds = config.AttackDelay * 1000;
            await Task.Delay((int)miliseconds);

            readyToAttack = true;
        }
    }

}