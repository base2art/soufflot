namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class FieldMapAttributeAttribute : Attribute
    {
        public bool Encoded { get; set; }
    }
}
