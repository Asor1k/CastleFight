using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight
{
    public class TalantScreenManager : MonoBehaviour
    {
        [SerializeField] private UnitTalantField[] unitTalantFields;
        [SerializeField] private UnitConfigsConfig[] unitConfigs;

        public void Init()
        {
            int j = 0;
            for (int i = 0; i < unitTalantFields.Length; i++)
            {
                unitTalantFields[i].Init(unitConfigs[j], unitConfigs[j+1]);
                j += 2;
            }
        }


    }
}