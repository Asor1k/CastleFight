using System.Collections.Generic;

namespace CastleFight.Core
{
    public class ManagerHolder
    {
        public static ManagerHolder I => instance ?? (instance = new ManagerHolder());

        private static ManagerHolder instance;

        public ManagerHolder()
        {
            this.managers = new List<object>();
        }

        private List<object> managers;

        public T GetManager<T>()
        {
            T result = default;
            foreach (var manager in managers)
            {
                result = (T) manager;
                if (result != null)
                {
                    return (T) manager;
                }
            }

            return result;
        }

        public void AddManager(object manager)
        {
            managers.Add(manager);
        }
    }
}