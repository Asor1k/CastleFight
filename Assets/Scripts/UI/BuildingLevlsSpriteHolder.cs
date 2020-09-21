using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight
{
    public class BuildingLevlsSpriteHolder : MonoBehaviour
    {
        public Sprite crownOff;
        public Sprite crownOn;
        public Sprite bigCrown;

        public void Awake()
        {
            ManagerHolder.I.AddManager(this);
        }

    }
}