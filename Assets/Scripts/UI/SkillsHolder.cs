using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CastleFight.Core;

namespace CastleFight.UI
{
    public class SkillsHolder : MonoBehaviour
    {
        [SerializeField]
        private List<SpellButton> spellButtons;

        void Start()
        {
            InitSpells();
        }

        private void InitSpells()
        {
            var spells = ManagerHolder.I.GetManager<UserSpellsHolder>().Spells;

            for (int i = 0; i < spells.Count; i++)
            {
                spellButtons[i].Init(spells[i]);
            }
        }
    }
}