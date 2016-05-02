namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class GETAttribute : Attribute
    {
        public string Path { get; set; }
    }
}
