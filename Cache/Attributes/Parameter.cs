using System;
using Nucleus.JetBrains;

namespace CacheAspect.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter), UsedImplicitly]
    public class IgnoreAttribute : Attribute
    {
    }
}
