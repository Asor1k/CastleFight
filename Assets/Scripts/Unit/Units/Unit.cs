using System;
using UnityEngine;
using CastleFight.Config;
using System.Collections.Generic;
using System.Collections;
using CastleFight.Core.EventsBus;
using CastleFight.Core;
using System.Threading.Tasks;
using CastleFight.Skills;

namespace CastleFight
{
    public abstract class Unit : MonoBehaviour
    {
        public event Action OnInit;
        public event Action OnKilled;
        public event Action OnReset;

        public bool Alive { get { return alive; } }
        public BaseUnitConfig Config { get { return config; } }
        public float CurrentSpeed { get { return agent.Speed; } }
        public UnitStats Stats { get { return stats; } }
        public Skill AttackSkill { get { return attackSkill; } }
        public Team Team => team;
        public IDamageable DamageBehaviour;

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
        [SerializeField]
        protected Skill attackSkill;

        protected Team team;
        protected IDamageable target;
        protected bool alive = true;
        protected bool readyToAttack = true;

        private AudioManager audioManager;
        private GoldManager goldManager;
        private Stat attackDelay;
        private Stat damage;

        public virtual void Init(BaseUnitConfig config, Team team)
        {
            DamageBehaviour = GetComponent<IDamageable>();

            this.team = team;
            gameObject.layer = (int)team;
            this.config = config;
            alive = true;
            stats.Init(config.Stats.ToArray());

            if(team==Team.Team1)
                InitPassiveAbilities();
            
            stats.OnHpChanged += OnUnitDamaged;
            goldManager = ManagerHolder.I.GetManager<GoldManager>();
            audioManager = ManagerHolder.I.GetManager<AudioManager>();
            agent.Init(this);
            attackSkill.Init(this);
            attackDelay = (Stat)stats.GetStat(StatType.AttackDelay);
            damage = (Stat)stats.GetStat(StatType.Damage);
            OnInit?.Invoke();
        }

        private void InitPassiveAbilities()
        {
            foreach (var ability in config.Abilities)
            {
                ability.Activate(this);
            }
        }
        public void Evade()
        {

        }
        public virtual void MoveTo(Vector3 point)
        {
            if (!alive) return;

            agent.MoveTo(point);
        }

        public virtual void Stop()
        {
            if (!alive) return;

            agent.Stop();
        }

        public virtual void Attack(IDamageable target)
        {
            if (!target.Alive || !alive || !readyToAttack) return;
            readyToAttack = false;
            agent.LookAt(target.Transform);
            
            MakeAttackSound();
            animationController.Attack
            (
                () =>
                {
                    if (!alive) return;
                    if (target.Type == TargetType.Building || target.Type == TargetType.Castle)
                    {
                        int gold = GetGoldPerHit();
                        goldManager.MakeGoldChange(gold, (Team)gameObject.layer);
                        goldManager.InitGoldAnim(gold, target.Transform);
                    }
                    attackSkill.SetTarget(target);
                    attackSkill.Execute();
                    MakeHitSound();
                },
                ()=> 
                {
                    StartAttackCooldown();
                }
            );
        }

        private void MakeAttackSound()
        { 
            audioManager.Play(config.UnitKind + " attack");
        }

        private void MakeHitSound()
        {
            if(config.UnitKind == UnitKind.Death)
            {
                audioManager.Play("Spit hit");
            }
            if (config.UnitKind == UnitKind.Knight)
            {
                audioManager.Play("Sword hit");
            }
            if (config.UnitKind == UnitKind.Skeleton)
            {
                audioManager.Play("Axe hit");
            }
        }

        private int GetGoldPerHit()
        {
            return Mathf.RoundToInt(damage.Value * config.goldDmgFraction);
        }

        private void Kill()
        {
            if (!alive) return;
            alive = false;
            collider.enabled = false;
            agent.Disable();
            healthBarCanvas.Show(false);
            EventBusController.I.Bus.Publish(new UnitDiedEvent(this));
            MakeDeathSound();
            if (gameObject.layer == (int)Team.Team2)
            {
                goldManager.InitGoldAnim(config.Cost, transform);
                MakeGoldSound();
            }
            OnKilled?.Invoke();
            DelayDestroy();
        }

        private void MakeDeathSound()
        {
            audioManager.Play(config.UnitKind+" death");
        }

        private void MakeGoldSound()
        {
            audioManager.Play("Gold for kill");
        }

        private async Task DelayDestroy()
        {
            await Task.Delay(3000);
            //Destroy(gameObject);
            gameObject.SetActive(false);
            Pool.I.Put(this);
        }

        public void Reset()
        {
            collider.enabled = true;
            readyToAttack = true;
            stats.Reset();
            agent.Enable();
            OnReset?.Invoke();
        }

        private void OnUnitDamaged(Stat stat)
        {
            if (stat.Value <= 0)
            {
                Kill();
            }
        }

        private async Task StartAttackCooldown()
        {
            var miliseconds = attackDelay.Value * 1000;
            await Task.Delay((int)miliseconds);

            readyToAttack = true;
        }
    }

}