using System;
using Nucleus.JetBrains;

namespace CacheAspect
{
    [AttributeUsage(AttributeTargets.Parameter), UsedImplicitly]
    public class IgnoreAttribute : Attribute
    {
    }
}
