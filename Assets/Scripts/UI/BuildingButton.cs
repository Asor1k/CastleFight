using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class BuildingButton : MonoBehaviour
    {
        [SerializeField] protected Building building;
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}