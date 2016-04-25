namespace Base2art.PlayN.Features.Linq
{
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.PlayN.Criteria;
    using Base2art.PlayN.Linq;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class QueryableEnumMatchesFeature
    {
        [Test]
        public void ShouldFilterByEnum()
        {
            var people = new List<Person>
                         {
                             new Person { Name = "Scott Youngblut", EyeColor = EyeColor.Brown },
                             new Person { Name = "Matt Youngblut", EyeColor = EyeColor.Green },
                             new Person { Name = "Glen Youngblut", EyeColor = EyeColor.Brown },
                         };

            var greens = people.AsQueryable().FilterEnum(x => x.EyeColor, new EnumCriteria<EyeColor> { EqualTo = EyeColor.Green });
            greens.Count().Should().Be(1);
            greens.First().Name.Should().Be("Matt Youngblut");
            
            var notBrowns = people.AsQueryable().FilterEnum(x => x.EyeColor, new EnumCriteria<EyeColor> { NotEqualTo = EyeColor.Brown });
            notBrowns.Count().Should().Be(1);
            notBrowns.First().Name.Should().Be("Matt Youngblut");

            var browns = people.AsQueryable().FilterEnum(x => x.EyeColor, new EnumCriteria<EyeColor> { EqualTo = EyeColor.Brown });
            browns.Count().Should().Be(2);
            browns.First().Name.Should().Be("Scott Youngblut");
            browns.Skip(1).First().Name.Should().Be("Glen Youngblut");
        }

        [Test]
        public void ShouldFilterByEnumNullChecks()
        {
            var people = new List<Person>
                         {
                             new Person { Name = "Scott Youngblut", EyeColor = EyeColor.Brown },
                             new Person { Name = "Matt Youngblut", EyeColor = EyeColor.Green },
                             new Person { Name = "Glen Youngblut", EyeColor = EyeColor.Brown },
                         };

            IQueryable<Person> asQueryable = people.AsQueryable();
            var greens = asQueryable.FilterEnum(x => x.EyeColor, null);
            greens.ShouldBeEquivalentTo(asQueryable);
        }

        private enum EyeColor
        {
            None,
            Brown,
            Blue,
            Green
        }

        private class Person
        {
            public string Name { get; set; }

            public EyeColor EyeColor { get; set; }
        }
    }
}
