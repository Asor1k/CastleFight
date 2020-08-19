using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public abstract class EffectConfig : ScriptableObject
    {
        public float Duration => duration;

        [SerializeField]
        protected float duration;

        public abstract VisualEffect Create();
    }
}