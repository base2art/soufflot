namespace Base2art.Soufflot.CommandRunner
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

    using Base2art.MonkeyTail;
    using Base2art.MonkeyTail.Config;
    using Base2art.MonkeyTail.Diagnostics;
    using Base2art.Soufflot.CommandRunner.Api.Util;

    // This is only for local development
    // In production this code is unused.
    // It comes from IIS7.5+
    public class SoufflotExecutor : Component
    {
        private readonly string directoryToWatch;

        private readonly IMessenger messenger;

        private readonly FileSystemWatcher watcher;

        private readonly object padLock = new object();

        private readonly string binPath;

        private readonly string webServerBinPath;

        private readonly int? port;

        private ICompiler viewCompiler;

        private TextWriter logFile;
        
        private WebServer webServer;

        private readonly IViewsSettings settings;
        public SoufflotExecutor(string directoryToWatch, IViewsSettings settings, IMessenger messenger, int? port)
        {
            this.settings = settings;
            this.port = port;
            this.directoryToWatch = directoryToWatch;
            this.messenger = messenger;
            this.binPath = Path.Combine(directoryToWatch, "target\\bin");
            this.webServerBinPath = Path.Combine(directoryToWatch, "target\\web-server-bin");
            Directory.CreateDirectory(this.binPath);
            Directory.CreateDirectory(this.webServerBinPath);
            this.watcher = new FileSystemWatcher(this.binPath, "*.dll");
        }

        public void Run()
        {
            this.watcher.Changed += (sender, args) => this.ReloadApp();
            this.watcher.Deleted += (sender, args) => this.ReloadApp();
            this.watcher.Created += (sender, args) => this.ReloadApp();
            this.watcher.Renamed += (sender, args) => this.ReloadApp();
            this.watcher.IncludeSubdirectories = false;
            this.watcher.EnableRaisingEvents = true;

            this.logFile = new StreamWriter(File.OpenWrite(Path.Combine(this.directoryToWatch, "Logs", DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss"))));
            
            this.viewCompiler = CompilerRunner.StartWatching(
                this.directoryToWatch,
                settings,
                new ConsoleMessenger(),
                new TextWriterLoggerWithFlush(this.logFile));
            
            this.viewCompiler.Compile();
            this.ReloadApp();

            Console.WriteLine("Watching Directory '{0}'...", directoryToWatch);
            Console.WriteLine("Press 'Control + Z' to exit");
            
            
            var isControlC = false;
            while (!isControlC)
            {
                var keyInfo = Console.ReadKey();
                isControlC = keyInfo.Key == ConsoleKey.Z
                && keyInfo.Modifiers == ConsoleModifiers.Control;
            }
        }


        private void ReloadApp()
        {
            lock (this.padLock)
            {
                this.watcher.EnableRaisingEvents = false;
                bool wasRunning = this.ShutdownApp();
                if (wasRunning)
                {
                    this.messenger.Info("Waiting to shutdown...");
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }

                this.StartupApp();
                this.watcher.EnableRaisingEvents = true;
            }
        }

        private bool ShutdownApp()
        {
            this.logFile.Flush();
            if (this.webServer != null)
            {
                this.messenger.Info("Shutting down the app...");

                this.webServer.Shutdown();

                this.messenger.Info("The app was shutdown...");
                return true;
            }

            return false;
        }

        private void StartupApp()
        {
            this.messenger.Info("Starting up the app...");
            var webServerBinDir = new DirectoryInfo(this.webServerBinPath);
            foreach (var fsInfo in webServerBinDir.EnumerateFiles())
            {
                try
                {
                    fsInfo.Delete();
                } catch (Exception e)
                {
                    this.messenger.Error(e.ToString());
                    //                    throw;
                }
            }

            foreach (var fsInfo in webServerBinDir.EnumerateDirectories())
            {
                fsInfo.Delete(true);
            }


            var binDir = new DirectoryInfo(this.binPath);
            binDir.CopyTo(webServerBinDir, copySubDirs: true);

            this.webServer = new WebServer(this.directoryToWatch, this.webServerBinPath, this.port);
            this.messenger.Info("The app was started...");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.ShutdownApp();
                this.watcher.Dispose();
                this.viewCompiler.Dispose();
                this.logFile.Dispose();
            }
        }
        
        
        public class TextWriterLoggerWithFlush : ILogger
        {
            private readonly TextWriter logger;

            public TextWriterLoggerWithFlush(TextWriter logger)
            {
                this.logger = logger;
            }

            public void Log(string message)
            {
                if (this.logger != null)
                {
                    this.logger.WriteLine(message);
                    this.logger.Flush();
                }
            }
        }
    }
}


/*
//            var appCtx = new ApplicationContext();
//
//            appCtx.ThreadExit += (sender, args) =>
//            {
//                Console.WriteLine("Closing...");
//            };
//
//            Application.Run(appCtx);*/