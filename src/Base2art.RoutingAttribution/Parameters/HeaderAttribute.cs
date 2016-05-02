namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class HeaderAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
