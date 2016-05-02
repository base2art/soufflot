
namespace Base2art.RoutingAttribution
{
	using System;

	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class PartMapAttribute : Attribute
	{
		ContentTransferEncoding Encoding
		{
			get;
			set;
		}
	}
}





