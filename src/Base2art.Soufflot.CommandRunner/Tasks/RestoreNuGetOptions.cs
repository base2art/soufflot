namespace Base2art.Soufflot.CommandRunner.Tasks
{
	using System;
	using CommandLine;

	[Verb("restore-nuget", HelpText = "Restore NuGet packages")]
	public class RestoreNuGetOptions
	{
        public string Directory { get; set; }

        public string NuGetPath { get; set; }
	}
}



