﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "TestBuilding", menuName = "Buildings/Test Building", order = 1)]
    public class TestBuildingConfig : BaseBuildingConfig
    {
        public override Building Create()
        {
            var building = Instantiate(_prefab);
            building.Init(_unit);

            return building;
        }
    }
}