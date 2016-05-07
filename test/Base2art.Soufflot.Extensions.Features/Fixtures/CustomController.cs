namespace Base2art.Soufflot.Fixtures
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class CustomController : IRenderingRouted
    {
        public IPositionedRenderingRouted[] RenderingControllers
        {
            get { return new IPositionedRenderingRouted[0]; }
        }

        public INonRenderingRouted[] NonRenderingControllers
        {
            get { return new INonRenderingRouted[0]; }
        }

        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            return new SimpleResult { Content = new SimpleContent { BodyContent = "Something!", ContentType = "text/plain" } };
        }

        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults, int i)
        {
            return new SimpleResult { Content = new SimpleContent { BodyContent = i + " - Something!", ContentType = "text/plain" } };
        }

        public IResult NotExecute(IHttpContext httpContext, List<PositionedResult> childResults, int i)
        {
            return new SimpleResult { Content = new SimpleContent { BodyContent = i + " - Something!", ContentType = "text/plain" } };
        }

        public IResult Index(IHttpContext a, List<PositionedResult> b)
        {
            throw new System.NotImplementedException();
        }

        public IResult Edit(IHttpContext a, List<PositionedResult> b, int i, string s)
        {
            throw new System.NotImplementedException();
        }

        public IResult ExecuteWithParams(IHttpContext ctx, List<PositionedResult> cld, int i, string path, int j)
        {
            throw new System.NotImplementedException();
        }
    }
}
