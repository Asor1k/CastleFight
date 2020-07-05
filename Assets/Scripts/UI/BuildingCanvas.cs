using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class BuildingCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        private void Awake()
        {
            canvas.worldCamera = Camera.main;
        }
    }
}