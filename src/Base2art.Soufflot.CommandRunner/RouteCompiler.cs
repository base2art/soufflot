namespace Base2art.Soufflot.CommandRunner
{
    using System;
    using System.ComponentModel;
    using System.IO;

    using System.Linq;
    using Base2art.MonkeyTail;
    using Base2art.MonkeyTail.Config;
    using Base2art.MonkeyTail.Diagnostics;

    public class RouteCompiler : Component
    {
        private readonly string fileToWatch;
        
        private readonly string workingDirectory;

        private readonly string routeOutputDir;
        
        private readonly IMessenger outputBuffer;

        private readonly ILogger logger;
        
        private readonly Lazy<FileSystemWatcher[]> fileSystemWatcher;
        
        public RouteCompiler(
            string workingDirectory,
            string fileToWatch,
            string routeOutputDir,
            IMessenger outputBuffer,
            ILogger logger)
        {
            this.workingDirectory = workingDirectory;
            this.fileToWatch = fileToWatch;
            this.routeOutputDir = routeOutputDir;
            this.outputBuffer = outputBuffer;
            this.logger = logger;
            
            this.fileTypes = FileTypes.KnownValues
                .Union(this.viewSettings.OutputFormats ?? new IFileType[0])
                .ToArray();
            
            this.fileSystemWatcher =
                new Lazy<FileSystemWatcher[]>(
                () => this.fileTypes.Select(x => new FileSystemWatcher(directoryToWatch, x.SearchPattern))
                    .ToArray());
            this.parser =
                new Lazy<Parser>(
                () =>
                    new Parser(
                    new TemplateEnumerator(this.directoryToWatch, this.fileTypes),
                    this.viewSettings,
                    this.workingDirectory,
                    this.viewsOutputDir));
        }

        public event EventHandler<CompileCompletedEventArgs> CompileCompleted;
        
        public void Run()
        {
            Array.ForEach(this.fileSystemWatcher.Value, x =>
                {
                    x.IncludeSubdirectories = true;
                    x.Changed += this.FileSystemWatcherChanged;
                    x.Created += this.FileSystemWatcherCreated;
                    x.Deleted += this.FileSystemWatcherDeleted;
                    x.Renamed += this.FileSystemWatcherRenamed;
                    x.EnableRaisingEvents = true;
                });
        }

        public CompileResult Compile()
        {
            Array.ForEach(this.fileSystemWatcher.Value, x =>
                {
                    x.EnableRaisingEvents = false;
                });

            var rezult = this.parser.Value.Parse(this.logger);
            
            Array.ForEach(rezult.Messages, x => this.outputBuffer.Info(x));
            Array.ForEach(rezult.Warnings, x => this.outputBuffer.Warning(x.Message));
            Array.ForEach(rezult.Errors, x => this.outputBuffer.Error(x.Message));

            if (rezult.ResultCode == 0)
            {
                this.outputBuffer.Info("Compile completed without errors...");
            }

            
            Array.ForEach(this.fileSystemWatcher.Value, x =>
                {
                    x.EnableRaisingEvents = true;
                });
            
            if (rezult.ResultCode != 0 || rezult.Errors.Length != 0)
            {
                return this.Completed(CompileResult.Failed);
            }
            
            if (rezult.Warnings.Length != 0)
            {
                return this.Completed(CompileResult.SuccessWithWarnings);
            }
            
            return this.Completed(CompileResult.Success);
        }

        protected virtual void OnCompileCompleted(CompileCompletedEventArgs e)
        {
            var handler = this.CompileCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (this.fileSystemWatcher.IsValueCreated)
                {
                    Array.ForEach(this.fileSystemWatcher.Value, x => x.Dispose());
                }
            }
        }

        private CompileResult Completed(CompileResult result)
        {
            this.OnCompileCompleted(new CompileCompletedEventArgs(result));
            return result;
        }
        
        private void FileSystemWatcherRenamed(object sender, RenamedEventArgs e)
        {
            this.Compile(e);
        }

        private void FileSystemWatcherDeleted(object sender, FileSystemEventArgs e)
        {
            this.Compile(e);
        }

        private void FileSystemWatcherCreated(object sender, FileSystemEventArgs e)
        {
            this.Compile(e);
        }

        private void FileSystemWatcherChanged(object sender, FileSystemEventArgs e)
        {
            this.Compile(e);
        }

        private void Compile(FileSystemEventArgs item)
        {
            this.Compile();
        }
    }
}
