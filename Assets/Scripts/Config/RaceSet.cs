using CastleFight;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "RaceSet", menuName = "Race/RaceSet", order = 0)]
    public class RaceSet : ScriptableObject
    {
        [SerializeField] private RaceConfig[] raceConfigs;

        public RaceConfig[] RaceConfigs => raceConfigs;
    }
}