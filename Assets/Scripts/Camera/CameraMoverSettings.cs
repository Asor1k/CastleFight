using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "CameraMoverSettings", menuName = "Settings/Camera/CameraMover", order = 1)]
    public class CameraMoverSettings : ScriptableObject
    {
        [SerializeField, Range(0f, 1f)] private float screenSensibility = 0.05f;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float timeUntillBoost = 2f;
        [SerializeField] private float boostedSpeed = 4f;
        public float ScreenSensibility => screenSensibility;
        public float Speed;

        public float BoostedSpeed => boostedSpeed;

        public float TimeUntillBoost => timeUntillBoost;
    }
}