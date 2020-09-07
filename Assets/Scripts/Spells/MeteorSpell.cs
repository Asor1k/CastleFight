using CastleFight.Core;
using CastleFight.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Spells
{
    public class MeteorSpell : Spell
    {
        [SerializeField]
        private MeteorProjectileConfig meteorConfig;
        [SerializeField]
        private Vector3 startOffset;
        [SerializeField]
        private float damage;

        public override void Execute(Vector3 targetPoint, Team team)
        {
            var projectile = meteorConfig.Create();
            var projectileStartPoint = targetPoint + startOffset;

            projectile.Launch(projectileStartPoint, team, targetPoint, OnProjectileHit);
        }

        private void OnProjectileHit(Projectile projectile)
        {
            Team targetTeam;

            if (projectile.OwnerTeam == Team.Team1)
            {
                targetTeam = Team.Team2;
            }
            else
            {
                targetTeam = Team.Team1;
            }

            var units = ManagerHolder.I.GetManager<UnitManager>().GetUnitsInRadius(projectile.transform.position, radius, targetTeam, false);

            foreach (var unit in units)
            {
                unit.TakeDamage(damage);
            }
        }
    }
}