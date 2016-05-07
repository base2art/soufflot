namespace Base2art.PlayN.Features.Linq
{
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.PlayN.Criteria;
    using Base2art.PlayN.Linq;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class QueryableStringContainsFeature
    {
        [Test]
        public void ShouldFilterByEnumNullChecks()
        {
            var people = new List<Person>
                         {
                             new Person { Name = "Scott Youngblut" },
                             new Person { Name = "Matt Youngblut" },
                             new Person { Name = "Glen Youngblut" },
                         };

            IQueryable<Person> qPeople = people.AsQueryable();
            var qPeople1 = qPeople.Filter(x => x.Name, null);
            qPeople1.ShouldBeEquivalentTo(qPeople);
        }

        [Test]
        public void ShouldFilterContainsCorrectly()
        {
            var table = new List<Person>();
            table.Add(new Person { Name = "Scott" });
            table.Add(new Person { Name = "Leat" });
            var result = table.AsQueryable().Filter(x => x.Name, new StringCriteria { Contains = "a" });
            result.Count().Should().Be(1);
            result.First().Name.Should().Be("Leat");
        }

        [Test]
        public void ShouldFilterContainsNotIgnoreCaseCorrectly()
        {
            var table = new List<Person>();
            table.Add(new Person { Name = "Scott" });
            table.Add(new Person { Name = "Leat" });
            var result = table.AsQueryable().Filter(x => x.Name, new StringCriteria { Contains = "A" });
            result.Count().Should().Be(0);
        }

        [Test, Ignore]
        public void ShouldFilterContainsIgnoreCaseCorrectly()
        {
            var table = new List<Person>();
            table.Add(new Person { Name = "Scott" });
            table.Add(new Person { Name = "Leat" });
            var stringCriteria = new StringCriteria { Contains = "A" };
            var result = table.AsQueryable().Filter(x => x.Name, stringCriteria);
            result.Count().Should().Be(1);
            result.First().Name.Should().Be("Leat");
        }

        private class Person
        {
            public string Name { get; set; }
        }
    }
}
