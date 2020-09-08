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
        public bool ReadyToAttack => readyToAttack;
        public Transform EffectPoint => effectPoint;

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
        [SerializeField]
        protected Transform effectPoint;

        protected Team team;
        protected IDamageable target;
        protected bool alive = true;
        protected bool readyToAttack = true;

        private GoldManager goldManager;
        private AudioManager audioManager;
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
            if (!target.Alive || !alive) return;

            agent.LookAt(target.Transform);
            if (!readyToAttack) return;

            readyToAttack = false;
            if (target.Type == TargetType.Building || target.Type == TargetType.Castle)
            {
                int gold = GetGoldPerHit();
                goldManager.MakeGoldChange(gold, (Team)gameObject.layer);
                if (gameObject.layer == (int)Team.Team1)
                    InitGoldAnim(config.Cost);
            }
            MakeAttackSound();
            animationController.Attack
            (
                () =>
                {
                    if (!alive) return;
                    MakeHitSound();
                    attackSkill.SetTarget(target);
                    attackSkill.Execute();
                },
                ()=> 
                {
                    StartAttackCooldown();
                }
            );
        }

        private int GetGoldPerHit()
        {
            return Mathf.RoundToInt(damage.Value * config.goldDmgFraction);
        }

        private void Kill()
        {
            alive = false;
            collider.enabled = false;
            agent.Disable();
            healthBarCanvas.Show(false);
            goldManager.MakeGoldChange(config.Cost, (Team)gameObject.layer == Team.Team1 ? Team.Team2 : Team.Team1);
            MakeDeathSound();
            EventBusController.I.Bus.Publish(new UnitDiedEvent(this));
            if (gameObject.layer == (int)Team.Team2)
            {
                goldManager.InitGoldAnim(config.Cost, transform);
            }
            OnKilled?.Invoke();
            DelayDestroy();
        }

        private async Task DelayDestroy()
        {
            await Task.Delay(3000);
            //Destroy(gameObject);
            gameObject.SetActive(false);
            Pool.I.Put(this);
        }
        
        private void MakeDeathSound()
        {
            audioManager.Play(config.UnitKind + " death");
            audioManager.Play("Gold for kill");
        }

        private void MakeHitSound()
        {
            switch(config.UnitKind)
            {
                case UnitKind.Knight:
                    audioManager.Play("Sword hit");
                    break;
                case UnitKind.Skeleton:
                    audioManager.Play("Axe hit");
                    break;
                case UnitKind.Death:
                    audioManager.Play("Spit hit");
                    break;
            }
        }

        private void MakeAttackSound()
        {
            audioManager.Play(config.UnitKind + " attack");
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

        private void InitGoldAnim(int gold)
        {
            GoldAnim goldAnim = Pool.I.Get<GoldAnim>();
            if (goldAnim == null)
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
            var miliseconds = attackDelay.Value * 1000;
            await Task.Delay((int)miliseconds);

            readyToAttack = true;
        }
    }

}