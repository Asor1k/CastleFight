using System;
using UnityEngine;
using CastleFight.Config;
using System.Collections.Generic;
using System.Collections;
using CastleFight.Core.EventsBus;
using CastleFight.Core;
using System.Threading.Tasks;

namespace CastleFight
{
    public abstract class  Unit : MonoBehaviour
    {
        public event Action OnInit;
        public event Action OnKilled;
        public event Action OnReset;

        public bool Alive{get{return alive;}}
        public BaseUnitConfig Config{get{return config;}}
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
        protected UnitStats stats;
        [SerializeField]
        protected BaseUnitConfig config;
        [SerializeField]
        protected UnitHealthBar healthBarCanvas;
        
        protected Team team;
        protected IDamageable target;
        protected int hp;
        protected int maxHp;
        protected bool alive = true;
        protected bool readyToAttack = true;

        private GoldManager goldManager;
        
        public virtual void Init(BaseUnitConfig config, Team team)
        {
            this.team = team;
            gameObject.layer = (int)team;
            this.config = config;
            maxHp = config.MaxHp;
            hp = maxHp;
            alive = true;
            stats.Init(config.MaxHp, config.Speed);
            stats.OnDamaged += OnUnitDamaged;
            goldManager = ManagerHolder.I.GetManager<GoldManager>();
            agent.Init(config);
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
            if(target.Type == TargetType.Building || target.Type == TargetType.Castle)
            {
                int gold = GetGoldPerHit();
                goldManager.MakeGoldChange(gold, (Team)gameObject.layer);
                if (gameObject.layer == (int)Team.Team1)
                    InitGoldAnim(config.Cost);
            }
            animationController.Attack(()=>{target.TakeDamage(config.Damage);});
        }
        
        private int GetGoldPerHit()
        {
            return Mathf.RoundToInt(config.Damage * config.goldDmgFraction);
        }
        
        private void Kill()
        {
            alive = false;
            collider.enabled = false;
            agent.Disable();
            healthBarCanvas.Show(false);
            goldManager.MakeGoldChange(config.Cost, (Team)gameObject.layer==Team.Team1?Team.Team2:Team.Team1);
            if(gameObject.layer == (int)Team.Team2) 
             InitGoldAnim(config.Cost);
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
        private void InitGoldAnim(int gold)
        {
            GoldAnim goldAnim = Pool.I.Get<GoldAnim>();
            if(goldAnim == null)
            {
                goldAnim = Instantiate(goldManager.goldAnimPrefab, transform);
            }
            else
            {
                goldAnim.gameObject.SetActive(true);
                goldAnim.transform.SetParent(transform);
                goldAnim.transform.localPosition = Vector3.zero;
            }
            goldAnim.Init(gold);
            StartCoroutine(DelayDestroyAnim(goldAnim));
        }

        private IEnumerator DelayDestroyAnim(GoldAnim anim)
        {
            yield return new WaitForSeconds(1);
            anim.gameObject.SetActive(false);
        }
        private async Task StartAttackCooldown()
        {
            var miliseconds = config.AttackDelay * 1000;
            await Task.Delay((int)miliseconds);

            readyToAttack = true;
        }
    }

}