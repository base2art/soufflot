namespace Base2art.Soufflot.Samples.Session
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Http.Util;
    using Base2art.Soufflot.Mvc;

    public class RedirectingController : SimpleRenderingController
    {
        protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            var value = httpContext.Request.QueryString.GetFirstOrEmpty("dest");
            return httpContext.Redirect(value);
        }
    }
}
