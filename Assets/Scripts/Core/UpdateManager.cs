using System;
using Core;
using UnityEngine;

namespace CastleFight.Core
{
    public class UpdateManager : MonoBehaviour, IUpdateManager
    {
        public event Action OnFixedUpdate;
        public event Action OnUpdate;
        public event Action OnLateUpdate;

        private void Awake()
        {
            ManagerHolder.I.AddManager(this);
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }
    }
}