using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CastleFight
{
    [CreateAssetMenu(fileName = "UnitSkill", menuName = "Skills/UnitSkill", order = 0)]
    public class BaseUnitSkillConfig : ScriptableObject
    {
        public bool IsActive => isActive;
       
        [SerializeField] private List<BaseSkillEffectConfig> skillEffects;

        private bool isActive;
        
        public void InitEffects(Unit unit)
        {
            foreach(BaseSkillEffectConfig baseSkillEffect in skillEffects)
            {
                baseSkillEffect.Init(unit);
            }
        }
    }
}
