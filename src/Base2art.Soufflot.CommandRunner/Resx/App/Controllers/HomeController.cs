namespace App.Controllers
{
    using Base2art.Soufflot.Mvc;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Api;
    using System.Collections.Generic;

    public class HomeController : SimpleRenderingController
    {
        protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            return httpContext.Ok(Views.Html.Home.Apply("My Title"));
        }

        public IResult AboutUs(IHttpContext b, List<PositionedResult> c)
        {
            return new SimpleResult
            {
                Content = new SimpleContent
                {
                    BodyContent = "My Title"
                }
            };
        }
    }
}
