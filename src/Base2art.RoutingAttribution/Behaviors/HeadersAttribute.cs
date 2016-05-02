
namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class HeadersAttribute : Attribute
    {
        public string[] Values { get; set; }
    }
}