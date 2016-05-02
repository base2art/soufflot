namespace Base2art.Soufflot.CommandRunner
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Base2art.MonkeyTail;
    using Base2art.MonkeyTail.Config;
    using Base2art.MonkeyTail.Diagnostics;
    using CommandLine;
    using Microsoft.Owin.Host.HttpListener;
    using Base2art.Soufflot.CommandRunner.Tasks;
    
    public class Program
    {
        private static int Main(string[] args)
        {
            var @default = CommandLine.Parser.Default;
            var result = @default.ParseArguments<RunOptions, GenerateOptions, RestoreNuGetOptions, InstallNuGetOptions>(args)
                           .MapResult(
                (RunOptions opts) => new RunTask(opts).Execute(),
                (GenerateOptions opts) => new GeneratorTask(opts).Execute(),
                (RestoreNuGetOptions opts) => new RestoreNuGetTask(opts).Execute(),
                (InstallNuGetOptions opts) => new InstallNuGetTask(opts).Execute(),
                errs => 1);
            
//            Console.ReadLine();
            
            return result;
        }
    }
}
