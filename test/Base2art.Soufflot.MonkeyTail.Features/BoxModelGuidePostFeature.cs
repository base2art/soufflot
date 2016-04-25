
namespace Base2art.Soufflot.Pack.Features
{
    using System;
	using Base2art.MonkeyTail.Api;
	using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class BoxModelGuidePostFeature
    {
        [Test]
        public void TestMethod()
        {
            BoxModelGuidePost.Center.Value.Should().Be(0);
            BoxModelGuidePost.Center.CompareTo(null).Should().Be(-1);
            BoxModelGuidePost.Center.CompareTo(BoxModelGuidePost.Center).Should().Be(0);
            BoxModelGuidePost.Right.CompareTo(BoxModelGuidePost.Left).Should().Be(1);
            BoxModelGuidePost.Left.CompareTo(BoxModelGuidePost.Right).Should().Be(-1);
            
            
            BoxModelGuidePost.Values[0].Value.Should().Be(0);
        }
    }
}
