namespace Base2art.Soufflot.Api.Fixtures
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Mvc;

    using Base2art.Soufflot.Http;

    public class CustomController : ICustomController
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
            return new SimpleResult { Content = new SimpleContent { BodyContent = "Something!", ContentType = "text/plain" } };
        }

        public IResult View(IHttpContext httpContext, List<PositionedResult> childResults, int i)
        {
            return new SimpleResult { Content = new SimpleContent { BodyContent = "Number i: " + i, ContentType = "text/plain" } };
        }
    }
}
