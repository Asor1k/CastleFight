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
        private int index = 0;

        public void Start()
        {
            talantUI = ManagerHolder.I.GetManager<NewTalantUI>();
        }
        

        public void InitDisabled(int index)
        {
            talantImage.sprite = talantUI.SkillSprites[(int)StatType.Unknown];
            frameImage.sprite = talantUI.FrameSprites[3 + (index / 2)];
            descriptionText.text = "";
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


        public void Init(StatModifier modifier, int index)
        {
            this.index = index;
            if(modifier == null)
            {
                return;
            }
            talantImage.sprite = talantUI.SkillSprites[(int)modifier.StatType];
            descriptionText.text = modifier.Value >= 0 ? "+ " + modifier.Value : " " + modifier.Value.ToString();
            frameImage.enabled = true;
            frameImage.sprite = talantUI.FrameSprites[6 + (index / 2)];
            descriptionText.text += " " + InsertSpaceBeforeUpperCase(modifier.StatType.ToString()).ToUpper();
        }
    }
}