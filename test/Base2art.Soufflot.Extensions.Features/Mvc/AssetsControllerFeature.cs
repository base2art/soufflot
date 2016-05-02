namespace Base2art.Soufflot.Mvc
{
    using System;
    using System.Collections.Generic;
    
    using System.IO;
    using Base2art.Soufflot.Api;
    
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class AssetsControllerFeature
    {
        [Test]
        public void ShouldNotLoadAssetOutOfSandBox()
        {
            var ctlr = new AssetsController();
            var ctx = new TestHttpContext();
            var rezult = ctlr.At(ctx, new List<PositionedResult>(), @"c:\Something\", @"path\..\..\");
            ctx.Response.StatusCode.Should().Be(404);
        }
        
        [Test]
        public void ShouldNotLoadAssetThatDoesntExist()
        {
            var ctlr = new AssetsController();
            var ctx = new TestHttpContext();
            var rezult = ctlr.At(ctx, new List<PositionedResult>(), @"c:\Temp\", Guid.NewGuid() + "\\index.html");
            ctx.Response.StatusCode.Should().Be(404);
        }
        
        [Test]
        public void ShouldLoadAssetThatExists()
        {
            var ctlr = new AssetsController();
            var ctx = new TestHttpContext();
            var path = Path.GetTempFileName();
            File.WriteAllText(path, "Scott Youngblut");
            var rezult = ctlr.At(ctx, new List<PositionedResult>(), Path.GetDirectoryName(path), Path.GetFileName(path));
            ctx.Response.StatusCode.Should().Be(200);
            rezult.Content.BodyAsString.Should().Be("Scott Youngblut");
        }
        
        [Test]
        public void ShouldVerifyPathWithDoubleSlash()
        {
            var ctlr = new AssetsController();
            ctlr.NormalizePath(@"path\\Index.html").Should().Be("path\\Index.html");
            ctlr.NormalizePath(@"path//Index.html").Should().Be("path\\Index.html");
            ctlr.NormalizePath(@"path\/Index.html").Should().Be("path\\Index.html");
        }
        
        [Test]
        public void ShouldVerifyPathWithDot()
        {
            var ctlr = new AssetsController();
            ctlr.NormalizePath(@"path\.\Index.html").Should().Be("path\\Index.html");
            ctlr.NormalizePath(@"path/./Index.html").Should().Be("path\\Index.html");
            ctlr.NormalizePath(@"path\./Index.html").Should().Be("path\\Index.html");
        }
        
        [Test]
        public void ShouldVerifyPathWithManyDot()
        {
            var ctlr = new AssetsController();
            ctlr.NormalizePath(@"path\.\.\Index.html").Should().Be("path\\Index.html");
            ctlr.NormalizePath(@"path/././Index.html").Should().Be("path\\Index.html");
            ctlr.NormalizePath(@"path\.\././Index.html").Should().Be("path\\Index.html");
        }
        
        [Test]
        public void ShouldVerifyPathWithDots()
        {
            var ctlr = new AssetsController();
            ctlr.NormalizePath(@"path\..\Index.html").Should().Be("Index.html");
            ctlr.NormalizePath(@"path/../Index.html").Should().Be("Index.html");
            ctlr.NormalizePath(@"path\../Index.html").Should().Be("Index.html");
        }
        
        [Test]
        public void ShouldVerifyPathWithManyDots()
        {
            var ctlr = new AssetsController();
            ctlr.NormalizePath(@"path\path2\..\..\Index.html").Should().Be("Index.html");
            ctlr.NormalizePath(@"path/path2/../../Index.html").Should().Be("Index.html");
            ctlr.NormalizePath(@"path\path2\../../Index.html").Should().Be("Index.html");
        }
        
        [Test]
        public void ShouldReturnNullWhenBreakingOutOfTheRoot()
        {
            var ctlr = new AssetsController();
            ctlr.NormalizePath(@"path\..\path2\..\..\Index.html").Should().BeNull();
        }
        
        [Test]
        public void ShouldVerifyPathWithInterspercedDots()
        {
            var ctlr = new AssetsController();
            ctlr.NormalizePath(@"root\path\..\path2\..\Index.html").Should().Be("root\\Index.html");
        }
    }
}
