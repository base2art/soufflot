namespace Base2art.Soufflot.Routing.Expressive
{
    using System;
    using System.Text.RegularExpressions;

    using Base2art.Soufflot.Api.Routing.Expressive;
    using Base2art.Soufflot.Http;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class ExpressiveRouteMatcherFeature
    {
        [Test]
        public void ShouldMatchStringsCorrectly()
        {
            ExpressiveRouteMatcher.IsMatch("SomeValue", null, StringComparison.OrdinalIgnoreCase)
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatch("SomeValue", "", StringComparison.OrdinalIgnoreCase)
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatch("SomeValue", "s", StringComparison.OrdinalIgnoreCase)
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatch("SomeValue", "someValue", StringComparison.OrdinalIgnoreCase)
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatch("SomeValue", "someValue", StringComparison.Ordinal)
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatch(null, null, StringComparison.OrdinalIgnoreCase)
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatch(null, "someValue", StringComparison.OrdinalIgnoreCase)
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatch("", "someValue", StringComparison.OrdinalIgnoreCase)
                .Should()
                .BeFalse();
        }

        [Test]
        public void ShouldMatchStringRegexsCorrectly()
        {
            ExpressiveRouteMatcher.IsMatchRegex("SomeValue", null)
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatchRegex("SomeValue", new Regex("S"))
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatchRegex("SomeValue", new Regex("z"))
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatchRegex("SomeValue", new Regex("someValue", RegexOptions.IgnoreCase))
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatchRegex("SomeValue", new Regex("someValue"))
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatchRegex(null, null)
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatchRegex(null, new Regex("someValue"))
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatchRegex("", new Regex("someValue"))
                .Should()
                .BeFalse();
        }

        [Test]
        public void ShouldMatchEnumsCorrectly()
        {
            ExpressiveRouteMatcher.IsMatch<HttpMethod>(HttpMethod.Get, HttpMethod.Get)
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatch<HttpMethod>(HttpMethod.Get, null)
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatch<HttpMethod>(null, null)
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatch<HttpMethod>(HttpMethod.Get, HttpMethod.Delete)
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatch<HttpMethod>(null, HttpMethod.Delete)
                .Should()
                .BeFalse();
        }

        [Test]
        public void ShouldMatchArrays()
        {
            ExpressiveRouteMatcher.IsMatch(new RouteExpressionParameter[0], new object[0])
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatch(
                new RouteExpressionParameter[0], 
                new object[1])
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatch(
                new RouteExpressionParameter[] { new ConstantRouteExpressionParameter("a")  }, 
                new object[] { "1" })
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatch(
                new RouteExpressionParameter[] { new ConstantRouteExpressionParameter("1")  }, 
                new object[] { "1" })
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatch(
                new RouteExpressionParameter[] { new ConstantRouteExpressionParameter(1) },
                new object[] { 1 })
                .Should()
                .BeTrue();

            ExpressiveRouteMatcher.IsMatch(
                new RouteExpressionParameter[] { new ConstantRouteExpressionParameter(2) },
                new object[] { 1 })
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatch(
                new RouteExpressionParameter[] { new ConstantRouteExpressionParameter("1") },
                new object[] { 1 })
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatch(
                new RouteExpressionParameter[] { new ConstantRouteExpressionParameter(null) },
                new object[] { 1 })
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatch(
                new RouteExpressionParameter[] { new ConstantRouteExpressionParameter(1) },
                new object[] { null })
                .Should()
                .BeFalse();

            ExpressiveRouteMatcher.IsMatch(
                new RouteExpressionParameter[] { new ConstantRouteExpressionParameter(null) },
                new object[] { null })
                .Should()
                .BeTrue();
        }
    }
}
