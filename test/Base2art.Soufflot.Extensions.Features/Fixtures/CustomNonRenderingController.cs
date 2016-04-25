namespace Base2art.Soufflot.Fixtures
{
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class CustomNonRenderingController : INonRenderingController
    {
        public INonRenderingController[] NonRenderingControllers
        {
            get
            {
                return new INonRenderingController[0];
            }
        }

        public void Execute(IHttpContext httpContext)
        {
        }

//        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults, int i)
//        {
//            return new SimpleResult { Content = new SimpleContent { Body = i + " - Something!", ContentType = "text/plain" } };
//        }
//
//        public IResult NotExecute(IHttpContext httpContext, List<PositionedResult> childResults, int i)
//        {
//            return new SimpleResult { Content = new SimpleContent { Body = i + " - Something!", ContentType = "text/plain" } };
//        }
    }
}