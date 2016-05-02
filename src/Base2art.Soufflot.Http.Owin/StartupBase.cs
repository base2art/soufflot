namespace Base2art.Soufflot.Http.Owin
{
    using System;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Config;
    using Base2art.Soufflot.Api.Diagnostics;
    using global::Owin;

    public abstract class StartupBase
    {
        public void Configuration(IAppBuilder app)
        {
            string path = app.BaseDirectory();
            
            var config = this.CreateConfiguration(path);
            
            IApplication application = ApplicationFinder.FindApplication(path, config);
            var manager = new RoutedExecutionManager(application, application.CreateRouter());
            var salt = this.GetSalt(application);
            ILogger logger = this.GetLogger(application);
            var appWireUps = application.CreateInstances(Class.GetClass<IApplicationExtender>(), true) ?? new IApplicationExtender[0];
            
            foreach (var wireUp in appWireUps)
            {
                if (wireUp != null)
                {
                    wireUp.Configure(app);
                }
            }
            
            app.Run(context =>
                    {
                        var content = context.ProcessRequest(manager, salt, logger);
                        if (content == null)
                        {
                            return context.Response.WriteAsync("Error...");
                        }
                        return context.Response.WriteAsync(content.Content.Body);
                    });
        }

        protected abstract IConfigurationProvider CreateConfiguration(string applicationPath);
        
        private ILogger GetLogger(IApplication application)
        {
            var loggerFactory = application.CreateInstance(Class.GetClass<ILogger>(), true);
            if (loggerFactory != null)
            {
                return loggerFactory;
            }
            var loggerFactoryClassName = application.ConfigurationValue(CommonSettings.LoggerFactoryClassNameKey);
            if (!string.IsNullOrWhiteSpace(loggerFactoryClassName))
            {
                try
                {
                    var factory = application.CreateInstance(Type.GetType(loggerFactoryClassName, false).GetClass().As<IRequestLoggerFactory>(), true);
                    if (factory != null)
                    {
                        var logLevel = application.ConfigurationValue(CommonSettings.LogLevelKey);
                        byte logLevelByte;
                        if (!byte.TryParse(logLevel, out logLevelByte))
                        {
                            logLevelByte = 0x00;
                        }
                        return factory.Create(new LogLevel(logLevelByte, "Default", "Default")) ?? new NullLogger();
                    }
                }
                catch (Exception)
                {
                }
            }
            return new NullLogger();
        }

        private string GetSalt(IApplication application)
        {
            var salt = application.ConfigurationValue(CommonSettings.SaltKey);
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new ApplicationException("You must set the salt in the configruation 'soufflot:salt'");
            }
            return salt;
        }
    }
}


