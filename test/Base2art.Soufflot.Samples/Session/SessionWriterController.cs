//namespace Base2art.Soufflot.Samples.Session
//{
//    using System.Collections.Generic;
//    using System.Linq;
//
//    using Base2art.Soufflot.Api;
//    using Base2art.Soufflot.Http;
//    using Base2art.Soufflot.Mvc;
//
//    public class SessionWriterController : SimpleRenderingController
//    {
//        protected override IEnumerable<INonRenderingRouted> NonRenderingControllers
//        {
//            get
//            {
//                yield return new SubController();
//            }
//        }
//
//        protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
//        {
//            return new SimpleResult { Content = new SimpleContent { BodyContent = "Value Set" } };
//        }
//
//        private class SubController : SimpleNonRenderingController
//        {
//            protected override void ExecuteMain(IHttpContext httpContext)
//            {
//                IHttpQueryString httpQueryString = httpContext.Request.QueryString;
//
//                if (httpQueryString.Contains("key-name") && httpQueryString.Contains("value"))
//                {
//                    string keyName = httpQueryString["key-name"].FirstOrDefault() ?? "value";
//                    string value = httpQueryString["value"].FirstOrDefault() ?? string.Empty;
//                    httpContext.Session[keyName] = value;
//                }
//            }
//        }
//    }
//}
