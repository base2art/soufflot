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
            provider.FindNonRenderingRoutedTypes(null).Should().BeNull();
            provider.FindRenderingRoutedType(null).Should().BeNull();
        }
    }
}
