
using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Base2art.Soufflot.RouteCompiler.Features
{
    [TestFixture]
    public class RouteCompilerFeature
    {
        [Test]
        public void Test()
        {
            var routeFile = @"";
            
            var routeCompiler = new RouteCompiler(routeFile, "AppName.Routes", "c:\\Temp\\");
            CompilerResult result = routeCompiler.Compile();
            result.Should().Be(CompilerResult.Success);
        }
    }
}