namespace Base2art.Soufflot.Http.Owin
{
    using Base2art.Soufflot.Api.Diagnostics;

    using FluentAssertions;

    using Microsoft.Owin;

    using NUnit.Framework;

    [TestFixture]
    public class RedirectFeature : AppBaseFeature
    {
        [Test]
        public void ShouldRedirect()
        {
            OwinContext context = OwinExtender.CreateRequestForPath("/redirect?dest=http://google.com");
            var result = context.ProcessRequest(this.Manager, null, this.CommonSalt, new NullLogger());
            result.Content.BodyAsString.Should().Be("Location: http://google.com");
            context.Response.Headers.Get("Location").Should().Be("http://google.com");
        }
    }

    
}