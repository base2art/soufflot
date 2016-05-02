namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class HTTPAttribute : Attribute
    {
        public string Method { get; set; }

        public string Path { get; set; }
    }
}
