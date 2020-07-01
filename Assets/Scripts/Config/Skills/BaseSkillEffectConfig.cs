using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    
    public abstract class BaseSkillEffectConfig : ScriptableObject
    {
        [SerializeField] private SkillEffect skillEffect;
        public virtual void Init(Unit unit) { }
    }
}