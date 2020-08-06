using System.Collections.Generic;

namespace PlatformFramework.EFCore.Context
{
    public class EntityConfigFlags
    {
        private readonly HashSet<string> _hashSet = new HashSet<string>();

        public bool Has(string key)
        {
            return _hashSet.Contains(key);
        }

        public void Set(string key)
        {
            if (!Has(key))
                _hashSet.Add(key);
        }
    }
}