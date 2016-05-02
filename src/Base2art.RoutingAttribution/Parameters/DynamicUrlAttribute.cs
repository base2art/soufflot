
namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class DynamicUrlAttribute : Attribute
    {
        string Url{ get; set; }
    }
}



