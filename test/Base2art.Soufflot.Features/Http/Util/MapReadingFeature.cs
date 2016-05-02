namespace Base2art.Soufflot.Http.Util
{
    using System.Linq;

    using Base2art.Collections;
    using Base2art.Soufflot.Http.Util;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class MapReadingFeature
    {
        [Test]
        public void ShouldGet()
        {
            var map = new Map<string, string>();
//            map.Add("MyKey", "a");
            map.Add("MyKey", "b");
            map.GetOrNull("No_KEY").Should().BeNull();

            map.GetOrEmpty("No_KEY").Should().BeEmpty();

            var vals = map.GetOrNull("MyKey");
            vals.Should().Be("b");

            vals = map.GetOrEmpty("MyKey");
            vals.Should().Be("b");
        }

//        [Test]
//        public void ShouldGetFirst()
//        {
//            var map = new Map<string, string>();
//            map.Add("MyKey", "a");
//            map.Add("MyKey", "b");
//            map.Get("No_KEY").Should().BeNull();
//
//            map.GetFirstOrEmpty("No_KEY").Should().BeEmpty();
//
//            var vals = map.GetFirstOrNull("MyKey");
//            vals.Should().Be("a");
//
//            vals = map.GetFirstOrEmpty("MyKey");
//            vals.Should().Be("a");
//        }
    }
}
