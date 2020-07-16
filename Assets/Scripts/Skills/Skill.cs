using System.Collections;
using System.Collections.Generic;
using CastleFight.Config;
using UnityEngine;

namespace CastleFight.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        protected BaseUnitConfig unitConfig;
        protected IDamageable target;
        
        public void SetTarget(IDamageable target)
        {
            this.target = target;
        }
        
        public void Init(BaseUnitConfig unitConfig)
        {
            this.unitConfig = unitConfig;
        }

        public abstract void Execute();
    }
}
