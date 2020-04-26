using System;

namespace Core
{
    public interface IUpdateManager
    {
        event Action OnFixedUpdate;
        event Action OnUpdate;
        event Action OnLateUpdate;
    }
}