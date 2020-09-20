using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using CastleFight.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight.UI
{
    public class BuildingLevelLabel : MonoBehaviour
    {

        [SerializeField] private int maxCrowns; 
        [SerializeField] private Image crownOff;
        [SerializeField] private Image crownOn;
        [SerializeField] private Image bigCrown;
        [SerializeField] private Transform crownParrent;
        [SerializeField] private HorizontalLayoutGroup layoutGroup;
        [SerializeField] private GameObject bigCrownBackHolder;

        private List<Image> crownsList = new List<Image>();

        public void SetLevel(int level, int maxLvl)
        {
            if(level >= maxLvl)
            {
                ClearParrent();
                Instantiate(bigCrown, crownParrent);
                layoutGroup.childControlHeight = false;
                layoutGroup.childControlWidth = false;
                layoutGroup.padding.left = 0;
                bigCrownBackHolder.SetActive(true);

            }
            else
            {
                ClearParrent();
                InstanceCrowns(level, maxLvl);
            }
        }

        private void ClearParrent()
        {
            foreach (Image img in crownsList)
            {
                Destroy(img.gameObject);
            }
            crownsList.Clear();
        }

        private void InstanceCrowns(int level, int maxLvl)
        {
            if (maxLvl == 3)
            {
                layoutGroup.padding.left = 5;
            }
            if (maxLvl == 4)
            {
                layoutGroup.padding.left = -17;
            }
            if(maxLvl == 5)
            {
                layoutGroup.padding.left = -35;
            }

            for (int i = 0; i < maxLvl-level; i++)
            {
                crownsList.Add(Instantiate(crownOff, crownParrent));
            }
            for (int j = 0; j < level; j++)
            {
                crownsList.Add(Instantiate(crownOn, crownParrent));
            }
        }
    }
}
