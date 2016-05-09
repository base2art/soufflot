namespace Base2art.Soufflot.Pack.Features
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Base2art.MonkeyTail.Api;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;
    
    public class ControllerApiFeature : SimpleRenderingRouted
    {
        protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            return httpContext.NoContent();
        }
        
        protected override IEnumerable<IPositionedRenderingRouted> RenderingRoutedItems
        {
            get
            {
                yield return this.CreateRenderingRouted(BoxModelGuidePost.BottomBottom, 1, new NullRenderingRouted());
                yield return this.CreateRenderingRouted(BoxModelGuidePost.BottomBottom, 1, (x, y) => x.Ok(new SimpleContent{ BodyContent = "String" }));
            }
        }
        
        protected override IEnumerable<INonRenderingRouted> NonRenderingRoutedItems
        {
            get
            {
                yield return this.CreateNonRenderingRouted(x => Debug.WriteLine(x.ToString()));
            }
        }
    }
}
