using System;
using System.Collections.Generic;
using CastleFight.Config;
using UnityEngine;

namespace CastleFight
{
    [Serializable]
    [CreateAssetMenu(fileName = "Config/UnitConfigs", menuName = "Configs", order = 0)]
    public class UnitConfigsConfig : ScriptableObject
    {
        public UnitKind unitKind;
        public List<BaseUnitConfig> unitConfigs = new List<BaseUnitConfig>();
    }
}
