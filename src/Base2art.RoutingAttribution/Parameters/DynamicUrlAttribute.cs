namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class DynamicUrlAttribute : Attribute
    {
        public string Url { get; set; }
    }
}
