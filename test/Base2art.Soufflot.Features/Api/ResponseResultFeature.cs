namespace Base2art.Soufflot.Api
{
    using System;
    using System.Net;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Config;
    using Base2art.Soufflot.Api.Diagnostics;

    using Base2art.Soufflot.Http.Owin;
    using FluentAssertions;

    using Microsoft.Owin;
    using NUnit.Framework;

    [TestFixture]
    public class ResponseResultFeature
    {
        [Test]
        public void ShouldSetValues()
        {
            var content = new SimpleContent { BodyContent = "<root>My Body</root>", ContentType = "text/xml" };
            var owinContext = new OwinContext();
            var httpContext = new HttpContext(this.App(), new NullLogger(), null, owinContext, new HttpContextSettings());

            var result = new ResponseResult(httpContext.Response, content);
            System.Text.Encoding.Default.GetString(result.Content.Body).Should().Be("<root>My Body</root>");
            result.Content.ContentType.Should().Be("text/xml");
            owinContext.Response.ContentType.Should().Be("text/xml");
        }

        [Test]
        public void ShouldSetNullContentType()
        {
            var content = new SimpleContent { BodyContent = "<root>My Body</root>" };
            var owinContext = new OwinContext();
            var httpContext = new HttpContext(this.App(), new NullLogger(), null, owinContext, new HttpContextSettings());

            var result = new ResponseResult(httpContext.Response, content);
            result.Content.BodyAsString.Should().Be("<root>My Body</root>");
            owinContext.Response.ContentType.Should().Be("text/plain");
        }

        [Test]
        public void ShouldSetNullContent()
        {
            var owinContext = new OwinContext();
            var httpContext = new HttpContext(this.App(), new NullLogger(), null, owinContext, new HttpContextSettings());

            var result = new ResponseResult(httpContext.Response, null);
            result.Content.BodyAsString.Should().BeEmpty();
            owinContext.Response.ContentType.Should().Be("text/plain");
        }

        [Test]
        public void ShouldSetValuesNullContentType()
        {
            var content = new SimpleContent { BodyContent = "<root>My Body</root>" };
            var owinContext = new OwinContext();
            var httpContext = new HttpContext(this.App(), new NullLogger(), null, owinContext, new HttpContextSettings());

            var result = new ResponseResult(httpContext.Response, content);
            result.WithLocation("http://google.com/")
                .WithStatusCode(HttpStatusCode.Accepted)
                .As("text/html");

            owinContext.Response.Headers.Get("Location").Should().Be("http://google.com/");
            owinContext.Response.StatusCode.Should().Be(202);
            owinContext.Response.ContentType.Should().Be("text/html");
        }

        private IApplication App()
        {
            return new Application(
                ApplicationMode.Prod, 
                Environment.CurrentDirectory,
                new AppDomainDataConfigurationProvider(AppDomain.CurrentDomain));
        }
    }
}
