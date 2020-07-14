
using System.Collections;
using System.Collections.Generic;
using CastleFight.Config;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "AngelLevel2", menuName = "Units/AngelLevel2", order = 0)]
    public class AngelLvl2Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<AngelLevel2>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as AngelLevel2;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}