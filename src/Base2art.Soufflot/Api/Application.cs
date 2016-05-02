namespace Base2art.Soufflot.Api
{
    using System;

    using Base2art.Soufflot.Api.Config;
    using Base2art.Soufflot.Api.Diagnostics;
    using Base2art.Validation;

    using Base2art.Soufflot.Http;

    public class Application : IApplication
    {
        private readonly ApplicationMode mode;

        private readonly string rootDirectory;

        private readonly IConfigurationProvider configuration;

        private readonly RetryLazy<bool> startupInvoker;

        private readonly RetryLazy<IComponentResolver> injector;

        private readonly RetryLazy<ILogger> applicationLogger;
        
        public Application(ApplicationMode mode, string rootDirectory, IConfigurationProvider configuration)
        {
            this.mode = mode;
            this.rootDirectory = rootDirectory;
            this.configuration = configuration;
            this.startupInvoker = new RetryLazy<bool>(() =>
                                                      {
                                                          this.OnApplicationStart();
                                                          return true;
                                                      });
            
            this.injector = new RetryLazy<IComponentResolver>(() => this.CreateServiceLoader());
            this.applicationLogger = new RetryLazy<ILogger>(() => this.CreateLogger());
        }

        public string RootDirectory
        {
            get
            {
                return this.rootDirectory;
            }
        }

        public ILogger ApplicationLogger
        {
            get
            {
                return this.applicationLogger.Value;
            }
        }

        protected virtual LogLevel ApplicationLogLevel
        {
            get
            {
                return this.Mode == ApplicationMode.Prod ? LogLevels.DeveloperFinest : LogLevels.ApplicationError;
            }
        }

        public ApplicationMode Mode
        {
            get
            {
                return this.mode;
            }
        }
        
        protected virtual IComponentResolver ApplicationInjector
        {
            get { return this.injector.Value; }
        }

        public TResult CreateInstance<TResult>(IClass<TResult> type, bool returnNullOnErrorOrNotFound)
            where TResult : class
        {
            type.Validate().IsNotNull();
            this.EnsureStartup();
            return this.CreateItemInstance(type, returnNullOnErrorOrNotFound);
        }

        public TResult[] CreateInstances<TResult>(IClass<TResult> type, bool returnNullOnErrorOrNotFound)
            where TResult : class
        {
            type.Validate().IsNotNull();
            this.EnsureStartup();
            return this.CreateItemInstances(type, returnNullOnErrorOrNotFound);
        }

        IResult IApplication.OnRouteNotFound(IHttpContext httpContext)
        {
            this.EnsureStartup();
            return this.OnRouteNotFound(httpContext);
        }

        IRouter IApplication.CreateRouter()
        {
            this.EnsureStartup();
            return this.CreateRouter();
        }

        string IApplication.ConfigurationValue(string key)
        {
            return this.ConfigurationValue(key);
        }

        protected virtual string ConfigurationValue(string key)
        {
            if (this.configuration == null)
            {
                throw new InvalidOperationException("No Configuration Provider Configured");
            }

            return this.configuration.GetValue(key);
        }

        protected virtual IResult OnRouteNotFound(IHttpContext httpContext)
        {
            return new SimpleResult {
                Content = new SimpleContent {
                    BodyContent = "Page Not Found",
                    ContentType = "text/plain"
                },
            };
        }

        protected virtual IRouter CreateRouter()
        {
            return this.CreateItemInstance(Class.GetClass<IRouter>(), true) ?? new NullRouter();
        }

        protected void EnsureStartup()
        {
            var val = this.startupInvoker.Value;
        }

        protected virtual void OnApplicationStart()
        {
        }

        protected virtual T CreateItemInstance<T>(IClass<T> type, bool returnNullOnErrorOrNotFound)
            where T : class
        {
            var itemInstance = this.ApplicationInjector.Resolve(type, returnNullOnErrorOrNotFound);
            return itemInstance;
        }

        protected virtual T[] CreateItemInstances<T>(IClass<T> type, bool returnNullOnErrorOrNotFound)
            where T : class
        {
            var itemInstance = this.ApplicationInjector.ResolveAll(type, returnNullOnErrorOrNotFound);
            return itemInstance ?? new T[0];
        }

        private IComponentResolver CreateServiceLoader()
        {
            return new InstantiatingComponentCreator(this);
            //            if (ServiceLoader.PrimaryLoader == null)
            //            {
            //                return new InstantiatorInjector(this);
            //            }
//
            //            return new AggregatingInstantiatorInjector(ServiceLoader.PrimaryLoader, new InstantiatorInjector(this));
        }

        private ILogger CreateLogger()
        {
            IApplicationLoggerFactory appLoggerFactory = this.CreateInstance(Class.GetClass<IApplicationLoggerFactory>(), true);
            if (appLoggerFactory == null)
            {
                if (this.Mode == ApplicationMode.Prod)
                {
                    return new NullLogger();
                }
                
                appLoggerFactory = new ConsoleLoggerFactory();
            }
            
            return appLoggerFactory.Create(this.ApplicationLogLevel);
        }
    }
}
