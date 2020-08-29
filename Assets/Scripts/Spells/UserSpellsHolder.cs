using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using CastleFight.Spells;
using UnityEngine;

namespace CastleFight
{
    public class UserSpellsHolder : MonoBehaviour
    {
        public List<Spell> Spells => spells;
        [SerializeField]
        private List<Spell> spells;

        private void Awake()
        {
            ManagerHolder.I.AddManager(this);
        }
    }
}