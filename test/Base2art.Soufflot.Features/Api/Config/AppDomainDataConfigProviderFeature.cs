namespace Base2art.Soufflot.Api.Config
{
    using System;

    using Base2art.Soufflot.Api.Config;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class AppDomainDataConfigProviderFeature
    {
        [Test]
        public void ShouldLoadConfig()
        {
            AppDomain.CurrentDomain.SetData("TEST:Item1", "Value1");
            AppDomain.CurrentDomain.SetData("TEST:Item2", "Value2");
            IConfigurationProvider appDomainProvider = new AppDomainDataConfigurationProvider(AppDomain.CurrentDomain);
            appDomainProvider.GetValue("TEST:Item1").Should().Be("Value1");
            appDomainProvider.GetValue("TEST:Item2").Should().Be("Value2");
            appDomainProvider.GetValue("TEST:Item3").Should().BeNull();
        }
    }
}