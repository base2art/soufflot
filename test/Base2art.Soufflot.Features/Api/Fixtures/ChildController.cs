namespace Base2art.Soufflot.Api.Fixtures
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Mvc;

    using Base2art.Soufflot.Http;

    public class ChildController : IRenderingRouted
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
                return new INonRenderingRouted[0];
            }
        }

        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            return new SimpleResult { Content = new SimpleContent { BodyContent = this.GetType().Name } };
        }
    }
}