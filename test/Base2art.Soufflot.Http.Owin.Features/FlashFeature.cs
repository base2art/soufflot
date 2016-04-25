namespace Base2art.Soufflot.Http.Owin
{
    using Base2art.Soufflot.Api.Diagnostics;

    using FluentAssertions;

    using Microsoft.Owin;

    using NUnit.Framework;

    [TestFixture]
    public class FlashFeature : AppBaseFeature
    {
        [Test]
        public void ShouldSaveAndLoadFlashWithOutRedirectIsEmpty()
        {
            OwinContext context = OwinExtender.CreateRequestForPath("/flash-set?key-name=scott&value=youngblut");
            context.ProcessRequest(this.Manager, null, this.CommonSalt, new InMemoryLogger(LogLevels.Off));

            OwinContext secondContext = OwinExtender.CreateRequestForPath("/flash-get?key-name=scott");
            secondContext.SetCookies(context.GetCookies());
            var result = secondContext.ProcessRequest(this.Manager, null, this.CommonSalt, new InMemoryLogger(LogLevels.Off));
            result.Content.BodyAsString.Should().Be("");
        }

        [Test]
        public void ShouldSaveAndLoadFlashWithRedirectKeepsValue()
        {
            OwinContext context = OwinExtender.CreateRequestForPath("/flash-set-with-redirect?key-name=scott&value=youngblut");
            context.ProcessRequest(this.Manager, null, this.CommonSalt, new InMemoryLogger(LogLevels.Off));

            OwinContext secondContext = OwinExtender.CreateRequestForPath("/flash-get?key-name=scott");
            secondContext.SetCookies(context.GetCookies());
            var result = secondContext.ProcessRequest(this.Manager, null, this.CommonSalt, new InMemoryLogger(LogLevels.Off));
            result.Content.BodyAsString.Should().Be("youngblut");
        }
    }
}