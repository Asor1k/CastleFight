using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight
{
    public class UnitTalantField : MonoBehaviour
    {
       
        [SerializeField] private TalantScreenCard[] upperTalantScreenCards;
        [SerializeField] private TalantScreenCard[] bottomTalantScreenCards;
        
        public void Init(UnitConfigsConfig upperUnit, UnitConfigsConfig bottomUnit)
        {
            for (int i = 0; i < upperTalantScreenCards.Length; i++)
            {
                if (upperUnit.unitConfigs[0].Abilities.Count > i)
                {
                    StatModifier upperModifier = upperUnit.unitConfigs[0].Abilities[i].Modifiers[0];
                    upperTalantScreenCards[i].Init(upperModifier,i);
                }
                else
                {
                    upperTalantScreenCards[i].InitDisabled(i);
                }
                if (bottomUnit.unitConfigs[0].Abilities.Count > i)
                {
                    StatModifier bottomModifier = bottomUnit.unitConfigs[0].Abilities[i].Modifiers[0];
                    bottomTalantScreenCards[i].Init(bottomModifier,i);
                }
                else
                {
                    bottomTalantScreenCards[i].InitDisabled(i);
                }
            }
        }

    }
}