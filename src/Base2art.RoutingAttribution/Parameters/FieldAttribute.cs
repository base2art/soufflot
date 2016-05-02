namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class FieldAttribute : Attribute
    {
        public string Name { get; set; }

        public bool Encoded { get; set; }
    }
}
