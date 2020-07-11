using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "MaxBuildings", menuName = "Settings/MaxBuildings", order = 0)]
    public class BuildingLimitConfig : ScriptableObject
    {
        public int MaxBuildingsPerTeam => maxBuildingsPerTeam;

        [SerializeField] private int maxBuildingsPerTeam;
    }
}