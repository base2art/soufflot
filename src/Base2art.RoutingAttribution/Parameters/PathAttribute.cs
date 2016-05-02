
namespace Base2art.RoutingAttribution
{
	using System;

	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class PathAttribute : Attribute
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

	
}





