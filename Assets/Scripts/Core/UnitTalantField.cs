using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight
{
    public class UnitTalantField : MonoBehaviour
    {
        /*[SerializeField] private Image[] upperImages;
        [SerializeField] private Image[] bottomImages;
        */
        [SerializeField] private TalantScreenCard[] upperTalantScreenCards;
        [SerializeField] private TalantScreenCard[] bottomTalantScreenCards;
        
        /*[SerializeField] private Image upperUnitImage;
        [SerializeField] private Image bottomUnitImage;
        [SerializeField] private TextMeshProUGUI[] upperTextDecriptions;
        [SerializeField] private TextMeshProUGUI[] bottomTextDecriptions;
        [SerializeField] private NewTalantUI talantUI;
        */
        public void Init(UnitConfigsConfig upperUnit, UnitConfigsConfig bottomUnit)
        {
            for (int i = 0; i < upperTalantScreenCards.Length; i++)
            {
                if (upperUnit.unitConfigs[0].Abilities.Count > i)
                {
                    StatModifier upperModifier = upperUnit.unitConfigs[0].Abilities[i].Modifiers[0];
                    upperTalantScreenCards[i].Init(upperModifier);
                }
                else
                {
                    upperTalantScreenCards[i].InitDisabled();
                }
                if (bottomUnit.unitConfigs[0].Abilities.Count > i)
                {
                    StatModifier bottomModifier = bottomUnit.unitConfigs[0].Abilities[i].Modifiers[0];
                    bottomTalantScreenCards[i].Init(bottomModifier);
                }
                else
                {
                    bottomTalantScreenCards[i].InitDisabled();
                }

                /*
                 * upperImages[i].sprite = talantUI.SkillSprites[(int)upperModifier.StatType];
                bottomImages[i].sprite = talantUI.SkillSprites[(int)bottomModifier.StatType];

                upperTextDecriptions[i].text = upperModifier.Value >= 0 ? "+ " + upperModifier.Value : " " + upperModifier.Value.ToString();
                bottomTextDecriptions[i].text = bottomModifier.Value >= 0 ? "+ " + bottomModifier.Value : " " + bottomModifier.Value.ToString();
                
                upperTextDecriptions[i].text += " " + upperModifier.StatType.ToString().ToUpper();
                bottomTextDecriptions[i].text += " " + bottomModifier.StatType.ToString().ToUpper();
                */
            }
        }

    }
}