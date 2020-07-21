using System.Collections;
using System.Collections.Generic;
using CastleFight.Config;
using UnityEngine;

namespace CastleFight.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        protected Unit unit;
        protected IDamageable target;
        
        public void SetTarget(IDamageable target)
        {
            this.target = target;
        }
        
        public virtual void Init(Unit unit)
        {
            this.unit = unit;
        }

        public abstract void Execute();
    }
}
