using CastleFight.Core;
using CastleFight.Spells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CastleFight.UI
{
    public class SpellButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image cooldownImage;
        [SerializeField]
        private Spell spell;
        private SpellController spellController;
        private bool isPointerHover;
        private bool isReady = true;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && isPointerHover && isReady)
            {
                OnBtnClick();
            }
        }

        private void Start()
        {
            spellController = ManagerHolder.I.GetManager<SpellController>();
            spellController.OnSpellCasted += OnSpellCasted;
        }

        private void OnBtnClick()
        {
            isReady = false;
            spellController.SetSpell(spell);
        }

        private void OnSpellCasted()
        {
            StartCoroutine(StartCooldown());
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isPointerHover = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerHover = false;
        }

        private IEnumerator StartCooldown()
        {
            float cooldownTime = 0;

            while (cooldownTime < spell.Cooldown)
            {
                cooldownTime += Time.deltaTime;
                cooldownImage.fillAmount = cooldownTime / spell.Cooldown;

                yield return null;
                
            }

            isReady = true;
        }
    }
}