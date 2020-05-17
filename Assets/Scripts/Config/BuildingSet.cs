using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "BuildingSet", menuName = "Buildings/BuildingSet", order = 1)]
    public class BuildingSet : ScriptableObject
    {
        [SerializeField] private BaseBuildingConfig[] buildingConfigs;

        public BaseBuildingConfig[] BuildingConfigs => buildingConfigs;
    }
}