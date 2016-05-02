namespace Base2art.Soufflot.Api
{
    using System;
    using System.Linq;

    using Base2art.Collections;
    using Base2art.Soufflot.Http.Util;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class HttpQueryStringFeature
    {
        [Test]
        public void ShouldLoadQueryStringMultiMapStandard()
        {
            var queryString = this.QsMultiMap("http://www.google.com?search=scott%20youngblut");
            queryString["search"].Count().Should().Be(1);
            queryString["search"].First().Should().Be("scott youngblut");
        }

        [Test]
        public void ShouldLoadQueryStringMultiMap0NoEqual()
        {
            var queryString = this.QsMultiMap("http://www.google.com?search");
            queryString["search"].Count().Should().Be(1);
            queryString["search"].First().Should().BeEmpty();
        }

        [Test]
        public void ShouldLoadQueryStringMultiMap1DuplicateEqual()
        {
            var queryString = this.QsMultiMap("http://www.google.com?search=sd=sd");
            queryString["search"].Count().Should().Be(1);
            queryString["search"].First().Should().Be("sd=sd");
        }

        [Test]
        public void ShouldLoadQueryStringMultiMap2DuplicateKey()
        {
            var queryString = this.QsMultiMap("http://www.google.com?search=Scott&search=Youngblut");
            queryString["search"].Count().Should().Be(2);
            queryString["search"].First().Should().Be("Scott");
            queryString["search"].Skip(1).First().Should().Be("Youngblut");
        }

        [Test]
        public void ShouldLoadQueryStringMultiMapEncodingIssues()
        {
            var queryString = this.QsMultiMap("http://www.google.com?key%20word=scott%20youngblut");
            queryString["key word"].Count().Should().Be(1);
            queryString["key word"].First().Should().Be("scott youngblut");
        }

        [Test]
        public void ShouldLoadQueryStringMultiMapFunky()
        {
            var queryString = this.QsMultiMap("http://www.google.com?&&&&");
            queryString.Keys.Count().Should().Be(0);
        }

        [Test]
        public void ShouldLoadQueryStringMapStandard()
        {
            var queryString = this.QsMap("http://www.google.com?search=scott%20youngblut");
            queryString["search"].Should().Be("scott youngblut");
        }

        [Test]
        public void ShouldLoadQueryStringMap0NoEqual()
        {
            var queryString = this.QsMap("http://www.google.com?search");
            queryString["search"].Should().BeEmpty();
        }

        [Test]
        public void ShouldLoadQueryStringMap1DuplicateEqual()
        {
            var queryString = this.QsMap("http://www.google.com?search=sd=sd");
            queryString["search"].Should().Be("sd=sd");
        }

        [Test]
        public void ShouldLoadQueryStringMap2DuplicateKey()
        {
            var queryString = this.QsMap("http://www.google.com?search=Scott&search=Youngblut");
//            queryString["search"].First().Should().Be("Scott");
            queryString["search"].Should().Be("Youngblut");
        }

        [Test]
        public void ShouldLoadQueryStringMapEncodingIssues()
        {
            var queryString = this.QsMap("http://www.google.com?key%20word=scott%20youngblut");
            queryString["key word"].Should().Be("scott youngblut");
        }

        [Test]
        public void ShouldLoadQueryStringMapFunky()
        {
            var queryString = this.QsMap("http://www.google.com?&&&&");
            queryString.Keys.Count().Should().Be(0);
        }

        [Test]
        public void ShouldWriteSimple()
        {
            var item = this.Create();
            item.Add("abc", "1");
            UrlEncodingExtender.Write(item).Should().Be("abc=1");
        }

        [Test]
        public void ShouldWriteMultipleValues()
        {
            var item = this.Create();
            item.Add("abc", "1");
            item.Add("def", "2");
            UrlEncodingExtender.Write(item).Should().Be("abc=1&def=2");
        }

        [Test]
        public void ShouldWriteEmptyValues()
        {
            var item = this.Create();
            item.Add("abc", string.Empty);
            item.Add("def", null);
            UrlEncodingExtender.Write(item).Should().Be("abc=&def=");
        }

        [Test]
        public void ShouldWriteEncodedValues()
        {
            var item = this.Create();
            item.Add("a bc", "'/");
            item.Add("def", "\"#");
            try
            {
                UrlEncodingExtender.Write(item).Should().Be("a%20bc=%27%2F&def=%22%23");
            }
            catch (Exception)
            {
                UrlEncodingExtender.Write(item).Should().Be("a%20bc='%2F&def=%22%23");
            }
        }

        private IMap<string, string> Create()
        {
            return new Map<string, string>();
        }

        private IReadOnlyMultiMap<string, string> QsMultiMap(string value)
        {
            var qs = new Uri(value).Query;
            return UrlEncodingExtender.ParseValue(qs);
        }

        private IReadOnlyMap<string, string> QsMap(string value)
        {
            var qs = new Uri(value).Query;
            var rez = new Map<string, string>();
            UrlEncodingExtender.ParseValue(rez, qs);
            return rez;
        }
    }
}
