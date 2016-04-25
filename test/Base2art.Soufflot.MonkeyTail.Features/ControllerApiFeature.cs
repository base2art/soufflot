
namespace Base2art.Soufflot.Pack.Features
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Base2art.MonkeyTail.Api;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;
    
    public class ControllerApiFeature : SimpleRenderingController
    {
        protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            return httpContext.NoContent();
        }
        
        protected override IEnumerable<IPositionedRenderingController> RenderingControllers
        {
            get
            {
                yield return this.CreateRenderingController(BoxModelGuidePost.BottomBottom, 1, new NullRenderingController());
                yield return this.CreateRenderingController(BoxModelGuidePost.BottomBottom, 1, (x, y) => x.Ok(new SimpleContent{ BodyContent = "String" }));
            }
        }
        
        protected override IEnumerable<INonRenderingController> NonRenderingControllers
        {
            get
            {
                yield return this.CreateNonRenderingController(x => Debug.WriteLine(x.ToString()));
            }
        }
    }
}
