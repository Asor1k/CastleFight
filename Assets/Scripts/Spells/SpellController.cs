using CastleFight.Core;
using CastleFight.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class SpellController : MonoBehaviour
    {
        public event Action OnSpellCasted;

        private Spell spell = null;
        [SerializeField]
        private LayerMask layerMask;
        private CameraMover cameraMover;

        private void Awake()
        {
            ManagerHolder.I.AddManager(this);
        }

        private void Start()
        {
            cameraMover = ManagerHolder.I.GetManager<CameraMover>();
        }

        public void SetSpell(Spell spell)
        {
            this.spell = spell;
        }

        public void Update()
        {
            if (spell == null) return;
            cameraMover.StopMoving();

            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, layerMask))
                {
                    spell.Execute(hit.point, Team.Team2);
                }

                spell = null;
                cameraMover.ContinueMoving();
                OnSpellCasted?.Invoke();
            }
        }
    }
}