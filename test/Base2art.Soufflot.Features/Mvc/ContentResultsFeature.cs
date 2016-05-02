namespace Base2art.Soufflot.Mvc
{
    using System;
    using System.Net;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Http.Util;
    using Base2art.Soufflot.Mvc;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class ContentResultsFeature
    {
        [Test]
        public void ShouldRenderResultsBadRequest()
        {
            this.Verify(HttpStatusCode.BadRequest, context => context.BadRequest());
            this.Verify(HttpStatusCode.BadRequest, context => context.BadRequest(new SimpleContent()));
            this.Verify(HttpStatusCode.BadRequest, context => context.BadRequest(null));

            this.Verify(HttpStatusCode.Created, context => context.Created());
            this.Verify(HttpStatusCode.Created, context => context.Created(new SimpleContent()));
            this.Verify(HttpStatusCode.Created, context => context.Created(null));

            this.Verify(HttpStatusCode.Forbidden, context => context.Forbidden());
            this.Verify(HttpStatusCode.Forbidden, context => context.Forbidden(new SimpleContent()));
            this.Verify(HttpStatusCode.Forbidden, context => context.Forbidden(null));

            this.Verify(HttpStatusCode.InternalServerError, context => context.InternalServerError());
            this.Verify(HttpStatusCode.InternalServerError, context => context.InternalServerError(new SimpleContent()));
            this.Verify(HttpStatusCode.InternalServerError, context => context.InternalServerError(null));

            this.Verify(HttpStatusCode.NoContent, context => context.NoContent());

            this.Verify(HttpStatusCode.NotFound, context => context.NotFound());
            this.Verify(HttpStatusCode.NotFound, context => context.NotFound(new SimpleContent()));
            this.Verify(HttpStatusCode.NotFound, context => context.NotFound(null));

            this.Verify(HttpStatusCode.OK, context => context.Ok());
            this.Verify(HttpStatusCode.OK, context => context.Ok(new SimpleContent()));
            this.Verify(HttpStatusCode.OK, context => context.Ok(null));

            this.Verify(HttpStatusCode.Unauthorized, context => context.Unauthorized());
            this.Verify(HttpStatusCode.Unauthorized, context => context.Unauthorized(new SimpleContent()));
            this.Verify(HttpStatusCode.Unauthorized, context => context.Unauthorized(null));

            this.Verify(HttpStatusCode.NotImplemented, context => context.Todo());

            this.Verify(HttpStatusCode.Ambiguous, context => context.Status((int)HttpStatusCode.Ambiguous));
            this.Verify(HttpStatusCode.Ambiguous, context => context.Status((int)HttpStatusCode.Ambiguous, new SimpleContent()));
            this.Verify(HttpStatusCode.Ambiguous, context => context.Status((int)HttpStatusCode.Ambiguous, null));
        }

        [Test]
        public void ShouldRenderRedirecting()
        {
            this.Verify("/abc", HttpStatusCode.Found, context => context.Found("/abc"));
            this.Verify("/abc", HttpStatusCode.MovedPermanently, context => context.MovedPermanently("/abc"));
            this.Verify("/abc", HttpStatusCode.Redirect, context => context.Redirect("/abc"));
            this.Verify("/abc", HttpStatusCode.SeeOther, context => context.SeeOther("/abc"));
            this.Verify("/abc", HttpStatusCode.TemporaryRedirect, context => context.TemporaryRedirect("/abc"));

            this.Verify("/abc", HttpStatusCode.Found, context => context.Found(new Route("/abc")));
            this.Verify("/abc", HttpStatusCode.MovedPermanently, context => context.MovedPermanently(new Route("/abc")));
            this.Verify("/abc", HttpStatusCode.Redirect, context => context.Redirect(new Route("/abc")));
            this.Verify("/abc", HttpStatusCode.SeeOther, context => context.SeeOther(new Route("/abc")));
            this.Verify("/abc", HttpStatusCode.TemporaryRedirect, context => context.TemporaryRedirect(new Route("/abc")));
        }

        private void Verify(string url, HttpStatusCode statusCode, Func<IHttpContext, IResult> func)
        {
            var response = this.Verify(statusCode, func);
            response.Headers.GetFirstOrNull("Location").Should().Be(url);
        }

        private TestHttpResponse Verify(HttpStatusCode statusCode, Func<IHttpContext, IResult> func)
        {
            return this.Verify((int)statusCode, func);
        }

        private TestHttpResponse Verify(int statusCode, Func<IHttpContext, IResult> func)
        {
            var context = this.CreateContext();
            var item = func(context);
            context.Response.StatusCode.Should().Be(statusCode);
            return context.Response;
        }

        private TestHttpContext CreateContext()
        {
            var context = new TestHttpContext();
            return context;
        }
    }
}
