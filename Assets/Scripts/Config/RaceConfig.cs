using UnityEngine;
using UnityEngine.UI;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "RaceConfig", menuName = "Race/RaceConfig", order = 1)]
    public class RaceConfig : ScriptableObject
    {
        [SerializeField] private Sprite disabledRaceSprite;
        [SerializeField] private Sprite enabledRaceSprite;
        [SerializeField] private Sprite enForeGroundSprite;
        [SerializeField] private Sprite disForeGroundSprite;
        [SerializeField] private string raceName;

        [SerializeField] private BuildingSet buildingSet;
        [SerializeField] private CastleConfig castle;
        [SerializeField] private Race race;
        public string RaceName => raceName;
        public Race Race =>race;
        public Sprite DisabledRaceSprite => disabledRaceSprite;
        public Sprite EnabledRaceSprite => enabledRaceSprite;
        public Sprite EnForeGroundSprite => enForeGroundSprite;
        public Sprite DisForeGroundSprite => disForeGroundSprite;
        public BuildingSet BuildingSet => buildingSet;
        public CastleConfig CastleConfig => castle;

        public bool Equals(RaceConfig config)
        {
            if (config == null || string.IsNullOrEmpty(config.raceName))
            {
                return false;
            }

            return raceName.Equals(config.raceName);
        }
    }

    public enum Race
    {
        Immortals,
        Kingdom
    }
}