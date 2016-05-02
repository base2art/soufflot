namespace Base2art.Soufflot.Api
{
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Mvc;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class SimpleResultFeature
    {
        [Test]
        public void ShouldReadBacking()
        {
            IResult result = new SimpleResult { Content = new SimpleContent { BodyContent = "ABC", ContentType = "text/plain" } };
            result.Content.As<SimpleContent>().BodyContent.Should().Be("ABC");
            result.Content.ContentType.Should().Be("text/plain");
//            result.ContentType.Should().Be("text/plain");

            result = result.As("text/html");
            result.Content.As<SimpleContent>().BodyContent.Should().Be("ABC");
            result.Content.ContentType.Should().Be("text/html");
//            result.ContentType.Should().Be("text/html");
        }
    }
}
