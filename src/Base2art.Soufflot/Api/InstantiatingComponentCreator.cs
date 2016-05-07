namespace Base2art.Soufflot.Api
{
    using System;
    using Base2art.Soufflot.Api.Diagnostics;

    public class InstantiatingComponentCreator : IComponentResolver
    {
        private readonly IApplication app;

        public InstantiatingComponentCreator(IApplication app)
        {
            this.app = app;
        }
        
        public T Resolve<T>(IClass<T> type, bool returnNullOnErrorOrNotFound)
        {
            return this.CreateInstanceByClass<T>(type, returnNullOnErrorOrNotFound);
        }

        public T[] ResolveAll<T>(IClass<T> type, bool returnNullOnErrorOrNotFound)
        {
            return new T[] { this.CreateInstanceByClass<T>(type, returnNullOnErrorOrNotFound) };
        }

        private T CreateInstanceByClass<T>(IClass<T> type, bool returnDefaultOnError)
        {
            var t = type.Type;
            return (T)this.CreateInstanceByType(type.Type, returnDefaultOnError);
        }

        private object CreateInstanceByType(Type type, bool returnDefaultOnError)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                if (!type.IsAssignableFrom(typeof(IApplicationLoggerFactory)))
                {
                    this.app.ApplicationLogger.Log(ex.ToString(), LogLevels.ApplicationDebug);
                }
                
                if (returnDefaultOnError)
                {
                    return null;
                }
                
                throw;
            }
        }
    }
}
