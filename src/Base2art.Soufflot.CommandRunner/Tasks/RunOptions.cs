namespace Base2art.Soufflot.CommandRunner.Tasks
{
    using System.Collections.Generic;
    using System.Linq;
    using Base2art.MonkeyTail;
    using Base2art.MonkeyTail.Config;
    using CommandLine;
    
	[Verb("run", HelpText = "Run a project")]
    public class RunOptions : IViewsSettings
    {
        // Omitting long name, default --verbose
        [Option('r', "reference", HelpText = "Dlls to reference")]
        public IEnumerable<Reference> References { get; set; }
        
        // Omitting long name, default --verbose
        [Option('f', "format", HelpText = "Dlls to reference")]
        public IEnumerable<FileType> OutputFormats { get; set; }

        [Option('i', "import", HelpText = "Classes to import into the views")]
        public IEnumerable<string> Imports { get; set; }

        [Option("linker", HelpText = "Path to f# compiler")]
        public string LinkerPath { get; set; }

        [Option('o', "output", HelpText = "The dll output file")]
        public string OutputFile { get; set; }

        [Option('v', "verbose", HelpText = "Turn on major logging")]
        public bool Verbose { get; set; }
        
        [Option("skip-secondary-output", HelpText = "Prevent the File from Being copied to alternate location for publishing")]
        public bool SkipSecondaryOutputFile { get; set; }

        [Option("secondary-output", HelpText = "The alternate location for publishing")]
        public string SecondaryOutputFile { get; set; }

        [Option("ildir", HelpText = "The Location for the parsed f# to be stored")]
        public string RelativeIntermediateDirectory { get; set; }

        [Option("sourcedir", HelpText = "The Location of the raw f#.html templates")]
        public string RelativeSourceDirectory { get; set; }
        
        [Option('d', "directory", HelpText = "The Base Directory")]
        public string Directory { get; set; }
        
        [Option('p', "port", HelpText = "Gets the Port", Default=58080)]
        public int Port { get; set; }
        
        [Option('s', "single-run", HelpText = "Sets a value indicating that only one run is perfromed")]
        public bool SingleRun { get; set; }

        IReference[] IViewsSettings.References
        {
            get { return (this.References ?? new Reference[0]).Select<Reference, IReference>(x => x).ToArray(); }
        }
        
        IFileType[] IViewsSettings.OutputFormats
        {
            get { return (this.OutputFormats ?? new FileType[0]).Select<FileType, IFileType>(x => x).ToArray(); }
        }

        string[] IViewsSettings.Imports
        {
            get { return (this.Imports ?? new string[0]).ToArray(); }
        }
    }
}
