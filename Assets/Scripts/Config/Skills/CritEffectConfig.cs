using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight {
    [CreateAssetMenu(fileName = "CritEffectConfig", menuName = "Skills/CritEffectConfig", order = 0)]
    public class CritEffectConfig : BaseSkillEffectConfig 
    {
        [SerializeField] private float critChance;
        [SerializeField] private float critMultiplier;
        
        public override void Init(Unit unit)
        {
            unit.stats.InitCritSkill(critChance, critMultiplier);
        }
    }
}