namespace Base2art.Soufflot.CommandRunner
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;

    using Config;

    using Microsoft.Owin.Hosting;
    using Microsoft.Owin.Hosting.Services;
    using Microsoft.Owin.Hosting.Starter;

    public class ApplicationRunner : Component, IApplicationRunner
    {
        private IDisposable item;

        public void Run(
            string rootDirectoryPath,
            string binPath,
            string currentDomainBin,
            int? port)
        {
            System.Console.WriteLine("Starting web server in '{0}'...", binPath);

            var currentDomain = this.CreateAppDomain(
                binPath,
                currentDomainBin);

            this.SetupAppDomain(currentDomain, rootDirectoryPath);

            IServiceProvider services = ServicesFactory.Create();

            IHostingStarter service = services.GetService<IHostingStarter>();

            var startOptions = new StartOptions
            {
                Port = port.GetValueOrDefault(58080),
                ServerFactory = "Microsoft.Owin.Host.HttpListener",
                AppStartup = typeof(Base2art.Soufflot.Http.Owin.Startup).FullName
            };

            try
            {
                this.item = service.Start(startOptions);
                Console.WriteLine("Started web server on port '{0}'...", startOptions.Port);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to start application");

                var targetInvocationException = e as TargetInvocationException;
                if (targetInvocationException != null && e.InnerException != null)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                    e = e.InnerException;
                }

                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        protected virtual void SetupAppDomain(
            AppDomain currentDomain,
            string rootDirectory)
        {
            var salt = string.Format(
                "!@#$%^&*()!@#$%^&*()QWERTYUIO@#123554656+{0}:{1}",
                Environment.MachineName,
                Environment.UserName);

            currentDomain.SetData(CommonSettingsWrapper.RootDirectoryKey, rootDirectory);
            currentDomain.SetData(CommonSettingsWrapper.SaltKey, salt);
            currentDomain.SetData(CommonSettingsWrapper.LoggerFactoryClassNameKey, CommonSettingsWrapper.ConsoleLoggerFactoryTypeName);
            currentDomain.SetData(CommonSettingsWrapper.LogLevelKey, "255");
            currentDomain.SetData(CommonSettingsWrapper.AppModeKey, "Dev");
        }

        protected virtual AppDomain CreateAppDomain(string binPath, string currentDomainBin)
        {
            var currentDomain = AppDomain.CurrentDomain;

            var appDomainAssemblyResolver = new AppDomainAssemblyResolver(binPath, currentDomainBin);
            currentDomain.AssemblyResolve += appDomainAssemblyResolver.ResolveAssembly;

            foreach (var dll in Directory.EnumerateFiles(binPath, "*.dll", SearchOption.TopDirectoryOnly))
            {
                var fnameNoExt = Path.GetFileNameWithoutExtension(dll);
                currentDomain.Load(fnameNoExt);
            }

            return currentDomain;
        }

        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.item.Dispose();
            }
        }
    }
}
