namespace Base2art.Soufflot.Api.Fixtures
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Mvc;

    using Base2art.Soufflot.Http;

    public class ChildController : IRenderingController
    {

        public IPositionedRenderingController[] RenderingControllers
        {
            get
            {
                return new IPositionedRenderingController[0];
            }
        }

        public INonRenderingController[] NonRenderingControllers
        {
            get
            {
                return new INonRenderingController[0];
            }
        }

        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            return new SimpleResult { Content = new SimpleContent { BodyContent = this.GetType().Name } };
        }
    }
}