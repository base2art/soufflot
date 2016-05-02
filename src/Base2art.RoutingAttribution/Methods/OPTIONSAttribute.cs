namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class OPTIONSAttribute : Attribute
    {
        public string Path { get; set; }
    }
}
