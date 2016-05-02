namespace Base2art.Soufflot.Api
{
    using Base2art.Soufflot.Api;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class NullRouterFeature
    {
        [Test]
        public void ShouldLoadWebConfig()
        {
            var provider = new NullRouter();
            provider.FindNonRenderingControllerTypes(null).Should().BeNull();
            provider.FindRenderingControllerType(null).Should().BeNull();
        }
    }
}
