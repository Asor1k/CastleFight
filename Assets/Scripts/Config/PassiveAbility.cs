using System;
using CastleFight.Config;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    [Serializable]
    [CreateAssetMenu(fileName = "Config/PassiveAbility", menuName = "Ability", order = 0)]
    public class PassiveAbility : ScriptableObject
    {
        public UnitKind unitKind;
        public List<Ability> abilities;
    }

}