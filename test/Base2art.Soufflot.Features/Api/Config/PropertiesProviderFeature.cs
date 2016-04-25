namespace Base2art.Soufflot.Api.Config
{
    using Base2art.Soufflot.Api.Config;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class PropertiesProviderFeature
    {
        [Test]
        public void ShouldLoadConfig()
        {
            var type = typeof(PropertiesProviderFeature);
            using (var resx = type.Assembly.GetManifestResourceStream(type, "Fixtures.PropertiesFile.txt"))
            {
                var provider = new PropertiesConfigurationProvider(resx);
                provider.GetValue("Key1").Should().Be("Value1");
                provider.GetValue("Key2").Should().Be("Value2");
                provider.GetValue("Key3").Should().Be("Value3");
                provider.GetValue(CommonSettings.SaltKey).Should().Be("SDFSDF@#$@#$SDVXFB@#$^%$^345345i423!(CVCBCVBXCVBCBCVBBVBXC$W#)$%:SF@#$");
                provider.GetValue("Key4").Should().BeNull();
            }
        }
    }
}