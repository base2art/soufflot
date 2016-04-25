namespace Base2art.Soufflot.CommandRunner.Tasks
{
	using System;
	using CommandLine;

	[Verb("new", HelpText = "Generate a project")]
	public class GenerateOptions
	{
        [Option('d', "directory", HelpText = "The Base Directory")]
        public string Directory { get; set; }

        [Option('a', "appName", HelpText = "The name of app", Required = true)]
        public string AppName { get; set; }
	}
}
