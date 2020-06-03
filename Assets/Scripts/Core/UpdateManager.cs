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
            if (OnUpdate == null) return;
            OnUpdate?.Invoke();
            
            foreach(EventHandler ev in OnUpdate.GetInvocationList())
            {
                Debug.Log(ev.Target);
            }
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }
    }
}