using CastleFight.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "Effects config", menuName = "Effects Config", order = 0)]
    public class EffectsConfig : ScriptableObject
    {
        public EffectConfig Vampirism => vampirism;
        [SerializeField]
        protected EffectConfig vampirism;
    }
}