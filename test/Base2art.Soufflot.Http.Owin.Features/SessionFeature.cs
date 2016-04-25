namespace Base2art.Soufflot.Http.Owin
{
    using Base2art.Soufflot.Api.Diagnostics;

    using FluentAssertions;

    using Microsoft.Owin;

    using NUnit.Framework;

    [TestFixture]
    public class SessionFeature : AppBaseFeature
    {

        [Test]
        public void ShouldSaveAndLoadSession()
        {
            OwinContext context = OwinExtender.CreateRequestForPath("/session-set?key-name=scott&value=youngblut");
            context.ProcessRequest(this.Manager, null, this.CommonSalt, new NullLogger());

            OwinContext secondContext = OwinExtender.CreateRequestForPath("/session-get?key-name=scott");
            secondContext.SetCookies(context.GetCookies());
            var result = secondContext.ProcessRequest(this.Manager, null, this.CommonSalt, new NullLogger());
            result.Content.BodyAsString.Should().Be("youngblut");
        }
    }
}

/*
            var httpContext = new HttpContext(null, context, new HttpContextSettings { ApplicationSaltSettings = "DEBUG" });
            httpContext.Unpack();
            manager.ExecuteController(httpContext);
            httpContext.Pack(false);*/