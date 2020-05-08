using UnityEngine;

namespace CastleFight
{
    public abstract class BuildingBehavior : MonoBehaviour
    {
        public abstract void MoveTo(Vector3 position);
    }
}
