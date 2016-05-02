namespace Base2art.Soufflot.Api
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;

    public interface IRenderingRouted
    {
        IPositionedRenderingRouted[] RenderingRoutedItems { get; }

        INonRenderingRouted[] NonRenderingRoutedItems { get; }

        IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults);
    }
}