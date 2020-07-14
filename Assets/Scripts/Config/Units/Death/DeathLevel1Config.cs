using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "DeathLevel1", menuName = "Units/DeathLevel1", order = 0)]
    public class DeathLevel1Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<DeathLevel1>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as DeathLevel1;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}