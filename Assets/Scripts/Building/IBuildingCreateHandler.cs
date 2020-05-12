using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public interface IBuildingCreateHandler
    {
        void MoveTo(Vector3 position);
        void Place();
        bool CanBePlaced();
    }
}