namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class PUTAttribute : Attribute
    {
        public string Path { get; set; }
    }
}
