namespace Base2art.Soufflot.CommandRunner
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    public class WebServer : Component
    {
        private readonly AppDomain childApp;

        public WebServer(string directoryToWatch, string binPath, int? port)
        {
            this.childApp = AppDomain.CreateDomain("MyAppDomain");
            var assemblyName = Assembly.GetExecutingAssembly().GetName().ToString();
            string typeName = typeof(ApplicationRunner).FullName;
            IApplicationRunner runner = (IApplicationRunner)this.childApp.CreateInstanceAndUnwrap(assemblyName, typeName);
            runner.Run(directoryToWatch, binPath, AppDomain.CurrentDomain.BaseDirectory, port);
//            this.isRunning = true;
        }

        public void Shutdown()
        {
            try
            {
                AppDomain.Unload(this.childApp);
            }
            catch (Exception)
            {
                // TODO: LOG
            }
//            this.isRunning = false;
        }
    }
}
