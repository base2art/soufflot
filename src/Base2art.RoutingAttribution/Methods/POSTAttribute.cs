namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class POSTAttribute : Attribute
    {
        public string Path { get; set; }
    }
}
