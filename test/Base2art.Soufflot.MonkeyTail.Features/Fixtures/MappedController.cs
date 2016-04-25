namespace Base2art.Soufflot.Pack.Features.Fixtures
{
    using System.Collections.Generic;
    using System.Text;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class MappedController : SimpleRenderingController
    {
        protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            var content = new MonkeyTail.ContentTypes.Html(new StringBuilder("Content For @{MonkeyTail}"));
            return httpContext.Ok(content);
        }
    }
}
