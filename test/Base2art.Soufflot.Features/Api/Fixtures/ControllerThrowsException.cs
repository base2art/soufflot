namespace Base2art.Soufflot.Api.Fixtures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class ControllerThrowsException : IRenderingRouted
    {
        public IPositionedRenderingRouted[] RenderingRoutedItems
        {
            get { return new IPositionedRenderingRouted[0]; }
        }

        public INonRenderingRouted[] NonRenderingRoutedItems
        {
            get { return new INonRenderingRouted[0]; }
        }

        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            throw new NotImplementedException();
        }
    }
}
