namespace Base2art.Soufflot.Http.Util
{
    using System.Linq;

    using Base2art.Collections;
    using Base2art.Soufflot.Http.Util;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class MultiMapReadingFeature
    {
        [Test]
        public void ShouldGet()
        {
            var map = new MultiMap<string, string>();
            map.Add("MyKey", "a");
            map.Add("MyKey", "b");
            map.GetOrNull("No_KEY").Should().BeNull();

            map.GetOrEmpty("No_KEY").Should().BeEmpty();

            var vals = map.GetOrNull("MyKey");
            vals.First().Should().Be("a");
            vals.Skip(1).First().Should().Be("b");

            vals = map.GetOrEmpty("MyKey");
            vals.First().Should().Be("a");
            vals.Skip(1).First().Should().Be("b");
        }

        [Test]
        public void ShouldGetFirst()
        {
            var map = new MultiMap<string, string>();
            map.Add("MyKey", "a");
            map.Add("MyKey", "b");
            map.GetFirstOrNull("No_KEY").Should().BeNull();

            map.GetFirstOrEmpty("No_KEY").Should().BeEmpty();

            var vals = map.GetFirstOrNull("MyKey");
            vals.Should().Be("a");

            vals = map.GetFirstOrEmpty("MyKey");
            vals.Should().Be("a");
        }
    }
}