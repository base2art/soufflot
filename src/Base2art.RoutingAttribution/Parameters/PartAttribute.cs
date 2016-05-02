
namespace Base2art.RoutingAttribution
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class PartAttribute : Attribute
    {
        string Name
        {
            get;
            set;
        }

        ContentTransferEncoding Encoding
        {
            get;
            set;
        }
    }

    
}



