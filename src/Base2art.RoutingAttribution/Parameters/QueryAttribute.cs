
namespace Base2art.RoutingAttribution
{
	using System;

	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class QueryAttribute : Attribute
	{
		string Name
		{
			get;
			set;
		}

		bool Encoded
		{
			get;
			set;
		}
	}

	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class QueryMapAttribute : Attribute
	{
		bool Encoded
		{
			get;
			set;
		}
	}
}







