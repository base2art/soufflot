namespace Base2art.PlayN.Features.Linq
{
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.PlayN.Criteria;
    using Base2art.PlayN.Linq;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class QueryableStringEndsWithFeature
    {
        [Test]
        public void ShouldFilterCorrectly()
        {
            var table = new List<Person>();
            table.Add(new Person { Name = "Truman" });
            table.Add(new Person { Name = "Leat" });
            var result = table.AsQueryable().Filter(x => x.Name, new StringCriteria { EndsWith = "n" });
            result.Count().Should().Be(1);
            result.First().Name.Should().Be("Truman");
        }

        [Test]
        public void ShouldFilterNotIgnoreCaseCorrectly()
        {
            var table = new List<Person>();
            table.Add(new Person { Name = "Truman" });
            table.Add(new Person { Name = "Leat" });
            var result = table.AsQueryable().Filter(x => x.Name, new StringCriteria { EndsWith = "N" });
            result.Count().Should().Be(0);
        }

        [Test, Ignore]
        public void ShouldFilterIgnoreCaseCorrectly()
        {
            var table = new List<Person>();
            table.Add(new Person { Name = "Truman" });
            table.Add(new Person { Name = "Leat" });
            var stringCriteria = new StringCriteria { EndsWith = "N" };
            var result = table.AsQueryable().Filter(x => x.Name, stringCriteria);
            result.Count().Should().Be(1);
            result.First().Name.Should().Be("Truman");
        }

        private class Person
        {
            public string Name { get; set; }
        }
    }
}