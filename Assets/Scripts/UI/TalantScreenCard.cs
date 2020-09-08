using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CastleFight.Core;
using System.Text;

namespace CastleFight
{
    public class TalantScreenCard : MonoBehaviour
    {
        [SerializeField] private Image talantImage;
        [SerializeField] private Image frameImage;
        [SerializeField] private TextMeshProUGUI descriptionText;

        private NewTalantUI talantUI;

        public void Start()
        {
            talantUI = ManagerHolder.I.GetManager<NewTalantUI>();
        }
        

        public void InitDisabled()
        {
            if (talantUI == null) Debug.Log("null");
            talantImage.sprite = talantUI.SkillSprites[(int)StatType.Unknown];
            frameImage.enabled = false;
            descriptionText.text = "UNKNOWN";
        }

        private string InsertSpaceBeforeUpperCase(string str)
        {
            var sb = new StringBuilder();

            char previousChar = char.MinValue; 
            foreach (char c in str)
            {
                if (char.IsUpper(c))
                {
                    if (sb.Length != 0 && previousChar != ' ')
                    {
                        sb.Append(' ');
                    }
                }
                sb.Append(c);
                previousChar = c;
            }
            return sb.ToString();
        }


        public void Init(StatModifier modifier)
        {
            if(modifier == null)
            {
                return;
            }
            talantImage.sprite = talantUI.SkillSprites[(int)modifier.StatType];
            descriptionText.text = modifier.Value >= 0 ? "+ " + modifier.Value : " " + modifier.Value.ToString();
            frameImage.enabled = true;
            descriptionText.text += " " + InsertSpaceBeforeUpperCase(modifier.StatType.ToString()).ToUpper();
        }

    }
}