namespace Base2art.Soufflot.Api.Fixtures
{
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Diagnostics;
    using Base2art.Soufflot.Mvc;

    using Base2art.Soufflot.Http;

    public class ParentController : IRenderingController
    {
        public IPositionedRenderingController[] RenderingControllers
        {
            get
            {
                return new IPositionedRenderingController[]
                       {
                           new PositionedRenderingController
                           {
                               Container = 0, 
                               ContainerPriority = 0,
                               RenderingController = new ChildController()
                           }
                       };
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
            httpContext.Logger.Log("Here?", LogLevels.Always);
            return new SimpleResult
                   {
                       Content =
                           new SimpleContent
                           {
                               BodyContent =
                                   "Parent Begin->"
                                   + string.Join(
                                       "-",
                                       childResults.Select(
                                           x => x.Result.Content.BodyAsString)) + "<- End"
                           }
                   };
        }
    }
}
