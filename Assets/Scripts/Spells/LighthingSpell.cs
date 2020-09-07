using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Spells
{
    public class LighthingSpell : Spell
    {
        [SerializeField]
        private List<ParticleSystem> effects;
        [SerializeField]
        private Transform origin;
        [SerializeField]
        private Vector3 startOffset;
        [SerializeField]
        private int damage;
        [SerializeField]
        private float damageDelay;
        private Team teamCache;
        private Vector3 targetPointCache;

        public override void Execute(Vector3 targetPoint, Team team)
        {
            origin.gameObject.SetActive(true);
            teamCache = team;
            targetPointCache = targetPoint;
            origin.position = targetPoint + startOffset;

            foreach (var effect in effects)
            {
                effect.time = 0;
                effect.Play();
            }

            DelayDamage();
        }

        private async Task DelayDamage()
        {
            await Task.Delay((int)(1000 * damageDelay));
            DealDamage();
        }

        private void DealDamage()
        {
            Team targetTeam;

            if (this.teamCache == Team.Team1)
            {
                targetTeam = Team.Team2;
            }
            else
            {
                targetTeam = Team.Team1;
            }

            var unit = ManagerHolder.I.GetManager<UnitManager>().GetClossestUnit(targetPointCache, radius, targetTeam, false);

            unit.TakeDamage(damage);
        }
    }
}