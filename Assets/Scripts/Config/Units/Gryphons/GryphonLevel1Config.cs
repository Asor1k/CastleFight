using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "GryphonLevel1", menuName = "Units/GryphonLevel1", order = 0)]
    public class GryphonLevel1Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<GryphonLevel1>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as GryphonLevel1;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}