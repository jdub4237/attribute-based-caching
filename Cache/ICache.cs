using System.Collections.Generic;
using Nucleus.JetBrains;

namespace CacheAspect
{
    public interface ICache
    {
        object this[string key] { get; set; }

        bool Contains(string key);

        void Delete(string key);

        [UsedImplicitly]
        void Clear();

        IEnumerable<string> Keys { get; }
    }
}
