namespace Base2art.Soufflot.Http.Util
{
    using System;
    using Base2art.Soufflot.Http.Util;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixtureAttribute]
    public class MimeMappingFeature
    {
        [Test]
        public void ShouldLoadMimeTypesAndMap()
        {
            IMimeMapping mapping = new MimeMapping();
            mapping.GetMimeMapping(string.Empty).Should().Be("text/plain");
            mapping.GetMimeMapping(null).Should().Be("text/plain");
            mapping.GetMimeMapping(" ").Should().Be("text/plain");
            
            mapping.GetMimeMapping("Abc").Should().Be("application/octet-stream");
            mapping.GetMimeMapping("scott/youngblut").Should().Be("application/octet-stream");
            
            mapping.GetMimeMapping("Abc.jpg").Should().Be("image/jpeg");
            mapping.GetMimeMapping("scott/youngblut.html").Should().Be("text/html");
            mapping.GetMimeMapping("scott/youngblut.css").Should().Be("text/css");
            mapping.GetMimeMapping("scott/youngblut.js").Should().Be("application/x-javascript");
        }
    }
}
