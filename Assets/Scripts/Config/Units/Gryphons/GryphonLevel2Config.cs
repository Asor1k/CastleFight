using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "GryphonLevel2", menuName = "Units/GryphonLevel2", order = 0)]
    public class GryphonLevel2Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<GryphonLevel2>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as GryphonLevel2;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}