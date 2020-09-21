using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "DeathLevel2", menuName = "Units/DeathLevel2", order = 0)]
    public class DeathLevel2Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<DeathLevel2>();

            if (unit == null)
            {
                unit = Instantiate(prefab) as DeathLevel2;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}