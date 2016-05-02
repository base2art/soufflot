namespace Base2art.Soufflot.Linq
{
    using System.Linq;

    using Base2art.Soufflot.Linq;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class ArrayExtensionFeature
    {
        [Test]
        public void ShouldCoalesceValueArray()
        {
            int[] items = null;

            var result1 = items.CoalesceValues();
            result1.Should().NotBeNull();
            result1.Count().Should().Be(0);

            var result2 = (new []{2,3}).CoalesceValues();
            result2.Should().NotBeNull();
            result2.Count().Should().Be(2);
        }

        [Test]
        public void ShouldCoalesceArray()
        {
            string[] items = null;

            var result = ArrayExtensions.Coalesce(items);
            result.Should().NotBeNull();
            result.Count().Should().Be(0);
        }

        [Test]
        public void ShouldCoalesceItems()
        {
            string[] items = {"a", "b"};

            var result = ArrayExtensions.Coalesce(items);
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            result.First().Should().Be("a");
            result.Skip(1).First().Should().Be("b");
        }
    }
}
