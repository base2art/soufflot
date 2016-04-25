namespace Base2art.Soufflot.Routing.Expressive
{
    using System.Text.RegularExpressions;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class ExpressiveRegexLiteralizerFeature
    {
        [Test]
        public void ShouldLiteral()
        {
            var regex = new Regex("/this-is-literal");
//            var literalizer = new ExpressiveRegexLiteralizer(regex);
//            literalizer.Literalize().Should().Be("/this-is-literal");
        }

        [Test]
        public void ShouldLoadDigit()
        {
            var regex = new Regex("/this-is-a-digit-\\d{1}");
//            var literalizer = new ExpressiveRegexLiteralizer(regex);
//            literalizer.Literalize().Should().Be("/this-is-a-digit-0");
        }
    }
}
