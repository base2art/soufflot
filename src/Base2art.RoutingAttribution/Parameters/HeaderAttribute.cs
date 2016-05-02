﻿
namespace Base2art.RoutingAttribution
{
	using System;

	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class HeaderAttribute : Attribute
	{
		string Name
		{
			get;
			set;
		}
	}
}


