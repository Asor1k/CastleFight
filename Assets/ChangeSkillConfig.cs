using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CastleFight {
    public class ChangeSkillConfig : MonoBehaviour
    {
        [SerializeField] private BaseUnitSkillConfig config;
        [SerializeField] private BaseSkillEffectConfig skill;
        public void AddSkill()
        {
            config.RemoveSkill(skill);
        }
    }
}