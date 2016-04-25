namespace Base2art.Soufflot.Api.Config
{
    using System;

    using System.IO;
    using Base2art.Soufflot.Api.Config;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class AggregateConfigProviderFeature
    {
        [Test]
        public void ShouldLoadConfig()
        {
            AppDomain.CurrentDomain.SetData("Key1", "AppDomain - Value1");
            AppDomain.CurrentDomain.SetData("TEST:Item2", "Value2");
            IConfigurationProvider appDomainProvider = new AppDomainDataConfigurationProvider(AppDomain.CurrentDomain);
            
            var type = typeof(PropertiesProviderFeature);
            IConfigurationProvider provider;
            using (var resx = type.Assembly.GetManifestResourceStream(type, "Fixtures.PropertiesFile.txt"))
            {
                var props = new PropertiesConfigurationProvider(resx);
                provider = new AggregateConfigurationProvider(appDomainProvider, props);
            }

            provider.GetValue("Key1").Should().Be("AppDomain - Value1");
            provider.GetValue("Key2").Should().Be("Value2");
            provider.GetValue("Key3").Should().Be("Value3");
            provider.GetValue(CommonSettings.SaltKey).Should().Be("SDFSDF@#$@#$SDVXFB@#$^%$^345345i423!(CVCBCVBXCVBCBCVBBVBXC$W#)$%:SF@#$");
            provider.GetValue("Key4").Should().BeNull();
            provider.GetValue("TEST:Item2").Should().Be("Value2");
        }
        
        [Test]
        public void ShouldLoadConfigFromFile()
        {
            AppDomain.CurrentDomain.SetData("Key1", "AppDomain - Value1");
            AppDomain.CurrentDomain.SetData("TEST:Item2", "Value2");
            IConfigurationProvider appDomainProvider = new AppDomainDataConfigurationProvider(AppDomain.CurrentDomain);
            
            var type = typeof(PropertiesProviderFeature);
            IConfigurationProvider provider;
            var path = Path.GetTempFileName();
            using (var resx = type.Assembly.GetManifestResourceStream(type, "Fixtures.PropertiesFile.txt"))
            {
                File.WriteAllText(path, new StreamReader(resx).ReadToEnd());
            }

            var props = new PropertiesConfigurationProvider(path);
            provider = new AggregateConfigurationProvider(appDomainProvider, props);
            provider.GetValue("Key1").Should().Be("AppDomain - Value1");
            provider.GetValue("Key2").Should().Be("Value2");
            provider.GetValue("Key3").Should().Be("Value3");
            provider.GetValue(CommonSettings.SaltKey).Should().Be("SDFSDF@#$@#$SDVXFB@#$^%$^345345i423!(CVCBCVBXCVBCBCVBBVBXC$W#)$%:SF@#$");
            provider.GetValue("Key4").Should().BeNull();
            provider.GetValue("TEST:Item2").Should().Be("Value2");
            
            try
            {
                File.Delete(path);
            // disable once EmptyGeneralCatchClause
            } catch (Exception)
            {
            }
        }
        
        [Test]
        public void ShouldLoadConfigFromFileNonExist()
        {
            AppDomain.CurrentDomain.SetData("Key1", "AppDomain - Value1");
            AppDomain.CurrentDomain.SetData("TEST:Item2", "Value2");
            IConfigurationProvider appDomainProvider = new AppDomainDataConfigurationProvider(AppDomain.CurrentDomain);
            
            var type = typeof(PropertiesProviderFeature);
            IConfigurationProvider provider;
            var path = Path.Combine(Path.GetTempPath(), "NonExist.Something");
            
            var props = new PropertiesConfigurationProvider(path);
            provider = new AggregateConfigurationProvider(appDomainProvider, props);
            provider.GetValue("Key1").Should().Be("AppDomain - Value1");
            provider.GetValue("Key2").Should().BeNull();
            provider.GetValue("Key3").Should().BeNull();
//            provider.GetValue(CommonSettings.SaltKey).Should().Be("SDFSDF@#$@#$SDVXFB@#$^%$^345345i423!(CVCBCVBXCVBCBCVBBVBXC$W#)$%:SF@#$");
            provider.GetValue("Key4").Should().BeNull();
            provider.GetValue("TEST:Item2").Should().Be("Value2");
        }
    }
}