using System.Collections;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using CastleFight.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight
{
    public class NewTalantUI : MonoBehaviour
    {
        public List<Sprite> SkillSprites => skillsSprites;

        [SerializeField] private Text unitNameText;
        [SerializeField] private Text modifierText;
        [SerializeField] private Image skillImage;
        [SerializeField] private List<Sprite> skillsSprites;
        private bool isActive = false;

        private void Awake()
        {
            ManagerHolder.I.AddManager(this);
        }

        void Start()
        {
            EventBusController.I.Bus.Subscribe<AbilityGeneratedEvent>(OnAbilityGenerated);
            Disable();
        }

        private void Enable()
        {
            unitNameText.gameObject.SetActive(true);
            modifierText.gameObject.SetActive(true);
            skillImage.gameObject.SetActive(true);
            isActive = true;
        }
        private void Disable()
        {
            unitNameText.gameObject.SetActive(false);
            modifierText.gameObject.SetActive(false);
            modifierText.text = "";
            skillImage.gameObject.SetActive(false);
            isActive = false;
        }
        private void OnAbilityGenerated(AbilityGeneratedEvent abilityGeneratedEvent)
        {
            Enable();
            unitNameText.text = abilityGeneratedEvent.unitKind.ToString();
            if(abilityGeneratedEvent.ability.Modifiers[0].Value >= 0)
            {
                modifierText.text += '+';
            }
            modifierText.text += abilityGeneratedEvent.ability.Modifiers[0].Value.ToString();
            if (skillsSprites[(int)abilityGeneratedEvent.ability.Modifiers[0].StatType] == null) return;
            skillImage.sprite = skillsSprites[(int)abilityGeneratedEvent.ability.Modifiers[0].StatType];
        }

        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<AbilityGeneratedEvent>(OnAbilityGenerated);
        }

        public void Update()
        {
            if (!isActive) return;
            if (Input.GetMouseButtonDown(0)) Disable();
        }
    }
}
