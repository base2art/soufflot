

namespace Base2art.Soufflot.CommandRunner.Tasks
{
    using System;
    using System.IO;
    using System.Linq;
    using Base2art.MonkeyTail;
    using Base2art.MonkeyTail.Config;
    using Base2art.MonkeyTail.Diagnostics;
    
    public class RunTask : TaskBase<RunOptions>
    {
        public RunTask(RunOptions opts)
            : base(opts)
        {
        }
        
        protected override int ExecuteInternalWithExitCode()
        {
            var opts = this.Options;
            var relativeProjectPath = "project/views.json";
            
            var directory = opts.Directory;
            
            if (string.IsNullOrWhiteSpace(directory))
            {
                directory = Environment.CurrentDirectory;
            }
            
            var settings = CompilerRunner.GetBuildSettings(directory, relativeProjectPath, new SharpJsonSerializer(), true);

            var newViewSettings = new ViewsSettings();
            IViewsSettings optsSettings = opts;
            newViewSettings.Imports = optsSettings.Imports.Union(settings.Imports).ToArray();
            newViewSettings.References = optsSettings.References.Union(settings.References)
                .Select(x => new Reference { HintPath = x.HintPath, Name = x.Name })
                .ToArray();
            
            
            newViewSettings.LinkerPath = Fold(opts, settings, x => x.LinkerPath);
            newViewSettings.OutputFile = Fold(opts, settings, x => x.OutputFile);
            newViewSettings.RelativeIntermediateDirectory = Fold(opts, settings, x => x.RelativeIntermediateDirectory);
            newViewSettings.RelativeSourceDirectory = Fold(opts, settings, x => x.RelativeSourceDirectory);
            newViewSettings.SecondaryOutputFile = Fold(opts, settings, x => x.SecondaryOutputFile);
            newViewSettings.SkipSecondaryOutputFile = opts.SkipSecondaryOutputFile || settings.SkipSecondaryOutputFile;
            
            
            if (opts.SingleRun)
            {
                var logFile = new StreamWriter(File.OpenWrite(Path.Combine(directory, "Logs", DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss"))));
            
                var logger = opts.Verbose
                        ? (ILogger)new TextWriterLogger(Console.Out)
                        : new NullLogger();
            
                using (var compiler = CompilerRunner.StartWatching(directory, settings, new ConsoleMessenger(), logger))
                {
                    if (compiler.Compile() == CompileResult.Success)
                    {
                        return -1;
                    }
                    
                    return -2;
                }
            }
            
            
            using (var executor = new PlayNExecutor(directory, settings, new ConsoleMessenger(), opts.Port))
            {
                executor.Run();
            }
            
            return 0;
        }
        
        protected override void ExecuteInternal()
        {
            throw new NotImplementedException();
        }

        private static string Fold(IViewsSettings opts, IViewsSettings settings, Func<IViewsSettings, string> par)
        {
            var nonDefault = par(opts);
            if (!string.IsNullOrWhiteSpace(nonDefault))
            {
                return nonDefault;
            }
            
            return par(settings);
        }
    }
}
