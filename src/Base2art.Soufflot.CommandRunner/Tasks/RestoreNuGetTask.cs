namespace Base2art.Soufflot.CommandRunner.Tasks
{
    using System;
    using System.IO;
    using Base2art.Soufflot.CommandRunner.Util;

    public class RestoreNuGetTask : TaskBase<RestoreNuGetOptions>
    {
        public RestoreNuGetTask(RestoreNuGetOptions opts)
            : base(opts)
        {
        }
	    
        protected override void ExecuteInternal()
        {
            
            var directory = this.Options.Directory;
            var nuGetPath = this.Options.NuGetPath;
            
            if (string.IsNullOrWhiteSpace(nuGetPath))
            {
                nuGetPath = Path.Combine(directory, "Tools\\nuget.exe");
            }
            
            
            var packagesPath = Path.Combine(directory, "project\\packages.config");
            var appSlnPath = Path.Combine(directory, "app.sln");
            var packagesDir = Path.Combine(directory, "project\\lib");
            
            var builderInstall = CommandExecutor.Builder()
                                                .WithWorkingDirectory(directory)
                                                .WithExecutable(nuGetPath)
                                                .WithParameters("install", packagesPath, "-o", packagesDir, "-NoCache")
                                                .Build()
                                                .ExecuteCommand();
            
            var builderRestore = CommandExecutor.Builder()
                                                .WithWorkingDirectory(directory)
                                                .WithExecutable(nuGetPath)
                                                .WithParameters("restore", packagesPath, "-o", packagesDir, "-NoCache")
                                                .Build()
                                                .ExecuteCommand();
            
            var builderSlnRestore = CommandExecutor.Builder()
                                                .WithWorkingDirectory(directory)
                                                .WithExecutable(nuGetPath)
                                                .WithParameters("restore", appSlnPath, "-o", packagesDir, "-NoCache")
                                                .Build()
                                                .ExecuteCommand();
        }
    }
}

