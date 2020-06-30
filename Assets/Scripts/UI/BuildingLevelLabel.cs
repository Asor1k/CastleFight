using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight.UI
{
    public class BuildingLevelLabel : MonoBehaviour
    {
        [SerializeField]
        private List<Image> levelIcons;

        public void SetLevel(int level)
        {
            for (int i = 0; i < levelIcons.Count; i++)
            {
                if (i <= level - 1)
                    levelIcons[i].gameObject.SetActive(true);
                else
                    levelIcons[i].gameObject.SetActive(false);
            }
        }
    }
}
