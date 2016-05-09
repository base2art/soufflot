namespace Base2art.Soufflot.Mvc
{
    using Base2art.Soufflot.Api;
    
    public class NullRenderingRouted : IRenderingRouted
    {
        IResult IRenderingRouted.Execute(Base2art.Soufflot.Http.IHttpContext httpContext, System.Collections.Generic.List<Base2art.Soufflot.Api.PositionedResult> childResults)
        {
            return new SimpleResult{ Content = new SimpleContent{ BodyContent = string.Empty } };
        }

        IPositionedRenderingRouted[] IRenderingRouted.RenderingRoutedItems
        {
            get { return new IPositionedRenderingRouted[0]; }
        }

        INonRenderingRouted[] IRenderingRouted.NonRenderingRoutedItems
        {
            get { return new INonRenderingRouted[0]; }
        }
    }
}
