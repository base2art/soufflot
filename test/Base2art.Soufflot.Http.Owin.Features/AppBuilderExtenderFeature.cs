namespace Base2art.Soufflot.Http.Owin
{
    using System;
    using System.Security.Policy;

    using Base2art.Soufflot.Api.Config;

    using FluentAssertions;

    using NUnit.Framework;
	using Base2art.Soufflot.Http.Owin.Fixtures;

    [TestFixture, Ignore("Difficult to test right now...; Justification='SjY'")]
    [Serializable]
    public class AppBuilderExtenderFeature
    {
        [Test]
        public void ShouldGetAppBuilderRootPath()
        {
            var type = typeof(DomainWrapper);

            var appDomainSetup = new AppDomainSetup { };
            appDomainSetup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            var domain = System.AppDomain.CreateDomain("TestDomain", new Evidence(), appDomainSetup);
            domain.SetData(CommonSettings.RootDirectoryKey, "c:\\Users\\Null\\");
            domain.Load(type.Assembly.GetName());
            var instance = (IDomainWrapper)domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
            instance.Root.Should().Be("c:\\Users\\Null\\");
        }

        [Test]
        public void ShouldGetAppBuilderRootPathNoPropSet()
        {
            var type = typeof(DomainWrapper);

            var appDomainSetup = new AppDomainSetup {};
            appDomainSetup.ApplicationBase = "c:\\Users\\null2\\";
            var old_base = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var domain = System.AppDomain.CreateDomain("TestDomain", new Evidence(), appDomainSetup);
            domain.SetData("FakeProp:basedir", old_base);
            
            domain.Load(type.Assembly.GetName());
            var instance = (IDomainWrapper)domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
            instance.Root.Should().Be("c:\\Users\\Null2\\");
        }
    }
}
/*"FakeProp:basedir"*/