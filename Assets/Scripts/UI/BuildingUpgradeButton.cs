using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CastleFight.Config;
using System;

namespace CastleFight.UI
{
    public class BuildingUpgradeButton : BuildingButton
    {
        public bool IsInited => isInited;
        [SerializeField] private Text costLabel;
        [SerializeField] private Image icon;
        [SerializeField] private Image frame;
        [SerializeField] private List<Sprite> frames;
        private bool isInited;


        public void SetLabels(Sprite iconImage, int cost, int level)
        {
            Debug.Log(cost);
            costLabel.text = cost.ToString();
            icon.sprite = iconImage;

            int frameIndex = 0;
            if (level - 1 < frames.Count)
            {
                frameIndex = level - 1;
            }
            else
            {
                frameIndex = frames.Count - 1;
            }

            frame.sprite = frames[frameIndex];
        }

        public void SetInited(bool status)
        {
            isInited = status;
        }

        public void OnButtonClick(int nodeIndex)
        {
            building.UpgradeBuilding(nodeIndex);
        }

    }
}
