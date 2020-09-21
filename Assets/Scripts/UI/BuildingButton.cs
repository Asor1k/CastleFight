using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class BuildingButton : MonoBehaviour
    {
        [SerializeField] protected Building building;
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetBuilding(Building building)
        {
            this.building = building;
        }
    }
}