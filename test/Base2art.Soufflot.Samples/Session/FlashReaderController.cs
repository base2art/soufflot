namespace Base2art.Soufflot.Samples.Session
{
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Http.Util;
    using Base2art.Soufflot.Mvc;

    public class FlashReaderController : SimpleRenderingController
    {
        protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            IHttpQueryString httpQueryString = httpContext.Request.QueryString;
            if (httpQueryString.Contains("key-name"))
            {
                string keyName = httpQueryString["key-name"].FirstOrDefault() ?? "value";
                return new SimpleResult { Content = new SimpleContent { BodyContent = httpContext.Flash.GetOrEmpty(keyName) } };
            }

            return new SimpleResult { Content = new SimpleContent { BodyContent = "Key Not Found" } };
        }
    }
}
