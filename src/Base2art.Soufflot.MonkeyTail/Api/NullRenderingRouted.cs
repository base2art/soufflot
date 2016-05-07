// <copyright company="Base2art">
// Copyright (c) 2014 All Rights Reserved
// </copyright>
// <author>Scott Youngblut</author>

namespace Base2art.MonkeyTail.Api
{
	using System;
	using Base2art.Soufflot.Api;
	using Base2art.Validation;

	internal class NullRenderingRouted : IRenderingRouted
	{
		IResult IRenderingRouted.Execute(Base2art.Soufflot.Http.IHttpContext httpContext, System.Collections.Generic.List<Base2art.Soufflot.Api.PositionedResult> childResults)
		{
			return new SimpleResult {
				Content = new SimpleContent {
					BodyContent = string.Empty
				}
			};
		}

		IPositionedRenderingRouted[] IRenderingRouted.RenderingRoutedItems
		{
			get
			{
				return new IPositionedRenderingRouted[0];
			}
		}

		INonRenderingRouted[] IRenderingRouted.NonRenderingRoutedItems
		{
			get
			{
				return new INonRenderingRouted[0];
			}
		}
	}
}


