
namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class PUTAttribute : Attribute
    {
        string Path
        {
            get;
            set;
        }
    }
}







