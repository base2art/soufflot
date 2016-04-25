namespace Base2art.Soufflot.Mvc
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;

    public interface IRenderingController
    {
        IPositionedRenderingController[] RenderingControllers { get; }

        INonRenderingController[] NonRenderingControllers { get; }

        IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults);
    }
}