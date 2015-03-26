using System;
using Nucleus.JetBrains;

namespace CacheAspect
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class UsePropertyAttribute : Attribute
    {
        [UsedImplicitly]
        private string _parameterValue;

        public UsePropertyAttribute(String parameterValue)
        {
            _parameterValue = parameterValue;
        }
    }
}
