namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class PartAttribute : Attribute
    {
        public string Name { get; set; }

        public ContentTransferEncoding Encoding { get; set; }
    }
}
