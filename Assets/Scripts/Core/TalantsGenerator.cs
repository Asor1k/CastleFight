using System;
using System.Collections.Generic;
using CastleFight.Config;
using CastleFight.Core;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEditor;
using UnityEngine;

namespace CastleFight
{
    public class TalantsGenerator : MonoBehaviour
    {

        [SerializeField] private List<PassiveAbility> passiveAbilities;
        [SerializeField] public int DefaultHighest => defaultHighest;
        [SerializeField] public int DefaultFraction => defaultFraction;
        public int UnitCount => unitCount;
        [SerializeField] private List<UnitConfigsConfig> unitConfigs = new List<UnitConfigsConfig>();

        public int unitCount;
        [SerializeField]private int defaultHighest;
        [SerializeField]private int defaultFraction;
        private PlayerProgress playerProgress;
        private List<int> weights = new List <int>();
        private List <int> talantLevels = new List<int>();
        private int maxLevels = 0;
        private int CurrLevels() {
            int n = 0;
            foreach (int i in talantLevels)
            {
                n += i;
            }
            return n;
        }

        public void Awake()
        {
            ManagerHolder.I.AddManager(this);
        }

        public void Start()
        {
            playerProgress = ManagerHolder.I.GetManager<PlayerProgress>();
            
            weights = playerProgress.Data.Weights;
            talantLevels = playerProgress.Data.TalantLevels;
            foreach (PassiveAbility pa in passiveAbilities)
            {
                maxLevels += pa.abilities.Count-1;   
            }
        }

        [ContextMenu("Reset configs")]
        public void ResetConfigs()
        {
            foreach(UnitConfigsConfig config in unitConfigs)
            {
                for(int i = 0; i < config.unitConfigs.Count; i++)
                {
                    config.unitConfigs[i].Abilities.Clear();
                }
            }
        }

        public void OnDestroy()
        {
            maxLevels = 0;
        }
        
        public void DebugGenerate()
        {
            StartGenerating();
        }

        public void StartGenerating()
        {
            int maxWeight = 1;
            for (int i = 0; i < weights.Count; i++)
            {
                maxWeight += weights[i];
                
            }
            int random = UnityEngine.Random.Range(0, maxWeight);
            int j = 0;
            Debug.Log(random);
            for (int i = 0; i < weights.Count; i++)
            {   
                j += weights[i];
                if (random <= j)
                {
                    GenerateTalant((UnitKind)i);
                   
                    return;
                }
            }
        }

        private void GenerateTalant(UnitKind unitKind)
        {
            weights[(int)unitKind] /= defaultFraction;
            talantLevels[(int)unitKind]++;
            Ability ability = new Ability();
            for(int i = 0; i < passiveAbilities.Count; i++)
            {
                if (passiveAbilities[i].unitKind == unitKind)
                {
                    if (passiveAbilities[i].abilities.Count <= talantLevels[(int)unitKind])
                    {
                        weights[(int)unitKind] *= defaultFraction;
                        talantLevels[(int)unitKind]--;
                        if (maxLevels <= CurrLevels())
                        {
                            Debug.LogError("There is no more levels to upgrade");
                            return;
                        }
                        StartGenerating();
                        return;
                    }
                        ability = passiveAbilities[i].abilities[talantLevels[(int)unitKind]];
                }
            }
            UnitConfigsConfig toChange = new UnitConfigsConfig();
            foreach(UnitConfigsConfig ucc in unitConfigs)
            {
                if(ucc.unitKind == unitKind)
                {
                    toChange = ucc;
                    break;
                }
            }

            foreach(BaseUnitConfig config in toChange.unitConfigs)
            {
                config.Abilities.Add(ability);
            }
            Debug.Log(unitKind);
            ShowAbility(ability, unitKind);
            playerProgress.SetTalants(talantLevels);
            playerProgress.SetWeigths(weights);
        }

        void ShowAbility(Ability ability, UnitKind unitKind)
        {
            if (ability == null)
            {
                Debug.LogError("NO ability exeption");
                return;
            }
            EventBusController.I.Bus.Publish(new AbilityGeneratedEvent(ability, unitKind));
        }
    }
}
