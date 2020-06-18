using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
using System;

namespace CastleFight
{
    public class Castle : MonoBehaviour
    {
          public event Action OnReady;
        public CastleConfig Config => config;

        private CastleConfig config;
        public void Init(CastleConfig config)
        {
            this.config = config;
        }

        public void Build()
        {
            //TODO: Implement building construction
            OnReady?.Invoke();
        }


        public bool Alive { get; }
        public Transform Transform { get; }

    }
}