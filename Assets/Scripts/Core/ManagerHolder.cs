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
            foreach (var manager in managers)
            {
                if (manager is T result)
                {
                    return result;
                }
            }

            return default;
        }
        public void Clear()
        {
            managers.Clear();
        }
        public void AddManager(object manager)
        {
            managers.Add(manager);
        }
    }
}