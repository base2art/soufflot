
namespace Base2art.RoutingAttribution
{
	using System;

	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class HTTPAttribute : Attribute
	{
		string Method
		{
			get;
			set;
		}

		string Path
		{
			get;
			set;
		}
	}
}





