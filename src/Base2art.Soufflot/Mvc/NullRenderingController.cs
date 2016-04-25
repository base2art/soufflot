namespace Base2art.Soufflot.Mvc
{
    using Base2art.Soufflot.Api;
    
    public class NullRenderingController : IRenderingController
    {
        IResult IRenderingController.Execute(Base2art.Soufflot.Http.IHttpContext httpContext, System.Collections.Generic.List<Base2art.Soufflot.Api.PositionedResult> childResults)
        {
            return new SimpleResult{ Content = new SimpleContent{ BodyContent = string.Empty } };
        }

        IPositionedRenderingController[] IRenderingController.RenderingControllers
        {
            get { return new IPositionedRenderingController[0]; }
        }

        INonRenderingController[] IRenderingController.NonRenderingControllers
        {
            get { return new INonRenderingController[0]; }
        }
    }
    
    
}
