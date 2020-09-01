using CastleFight.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class ConfigsController : MonoBehaviour
    {
        [SerializeField]
        private EffectsConfig config;

        private void Awake()
        {
            ManagerHolder.I.AddManager(config); ;
        }
    }
}