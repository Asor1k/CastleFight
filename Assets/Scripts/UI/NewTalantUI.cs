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
        public List<Sprite> FrameSprites => frameSprites;

        [SerializeField] private Text unitNameText;
        [SerializeField] private Text modifierText;
        [SerializeField] private Image skillImage;
        [SerializeField] private Image unitImage;
        [SerializeField] private Image frameImage;
        [SerializeField] private Image blur;
        [SerializeField] private Canvas canvas;
        [SerializeField] private TalantsGenerator talantsGenerator;

        [SerializeField] private List<Sprite> skillsSprites;
        [SerializeField] private List<Sprite> frameSprites;

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
            canvas.enabled = true;
            blur.enabled = true;
            isActive = true;
        }

        private void Disable()
        {
            canvas.enabled = false;
            blur.enabled = false;
            modifierText.text = "";
            isActive = false;
        }

        private void OnAbilityGenerated(AbilityGeneratedEvent eventData)
        {
            Enable();
            unitNameText.text = eventData.unitKind.ToString();
            if(eventData.ability.Modifiers[0].Value >= 0)
            {
                modifierText.text += '+';
            }
            modifierText.text += eventData.ability.Modifiers[0].Value.ToString();
            if (skillsSprites[(int)eventData.ability.Modifiers[0].StatType] == null) return;
            skillImage.sprite = skillsSprites[(int)eventData.ability.Modifiers[0].StatType];
            unitImage.sprite = talantsGenerator.UnitConfigs[(int)eventData.unitKind].unitConfigs[0].Icon;
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
