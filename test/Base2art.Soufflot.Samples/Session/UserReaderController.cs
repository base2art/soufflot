namespace Base2art.Soufflot.Samples.Session
{
    using System.Collections.Generic;
    using System.Linq;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class UserReaderController : SimpleRenderingController
    {
        protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            var user = httpContext.Request.User;
            var userName = user.UserName;
            return new SimpleResult
            {
                Content = new SimpleContent
                {
                    BodyContent = userName + "[" + string.Join(",", user.GroupNames) + "]" + user.IsAuthenticated
                }
            };
        }
    }
}
