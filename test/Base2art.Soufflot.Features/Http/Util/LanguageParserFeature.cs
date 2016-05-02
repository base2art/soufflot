namespace Base2art.Soufflot.Http.Util
{
    using System.Globalization;

    using Base2art.Soufflot.Http.Util;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class LanguageParserFeature
    {
        [Test]
        public void ShouldParseCorrectString()
        {
            CultureInfo[] cultureInfos = LanguageExtender.ToCultures("bg-BG,en-US;q=0.7,ar-BH;q=0.3");
            cultureInfos.Length.Should().Be(3);
        }

        [Test]
        public void ShouldFailParseOnNonCorrectString()
        {
            CultureInfo[] cultureInfos = LanguageExtender.ToCultures("bgdfdsdf,en-US;q=0.7,ar-BH;q=0.3");
            cultureInfos.Length.Should().Be(2);
        }
    }
}
