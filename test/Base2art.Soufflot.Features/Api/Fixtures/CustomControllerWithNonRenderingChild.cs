namespace Base2art.Soufflot.Api.Fixtures
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Mvc;

    using Base2art.Soufflot.Http;

    public class CustomControllerWithNonRenderingChild : ICustomController
    {
        public IPositionedRenderingRouted[] RenderingControllers
        {
            get
            {
                return new IPositionedRenderingRouted[0];
            }
        }

        public INonRenderingRouted[] NonRenderingControllers
        {
            get
            {
                return new INonRenderingRouted[] { new CountingNonRenderingController() };
            }
        }

        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            return new SimpleResult { Content = new SimpleContent { BodyContent = "Something!", ContentType = "text/plain" } };
        }
    }
}