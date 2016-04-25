namespace Base2art.Soufflot.CommandRunner.Util
{
    using System;
    using System.Linq;

    public class CommandExecutorBuilder
    {
        private string directory;

        private string path;

        private string parameters;

        public string Parameters
        {
            get { return this.parameters ?? ""; }
        }

        public string Executable
        {
            get { return this.path; }
        }

        public string WorkingDirectory
        {
            get { return this.directory; }
        }
        
        public CommandExecutorBuilder WithWorkingDirectory(string directory)
        {
            this.directory = directory;
            return this;
        }
        
        public CommandExecutorBuilder WithExecutable(string path)
        {
            this.path = path;
            return this;
        }
        
        public CommandExecutorBuilder WithParameters(string parameters)
        {
            this.parameters = parameters;
            return this;
        }
        
        public CommandExecutorBuilder WithParameters(params string[] value)
        {
            this.parameters = string.Join(" ", (value ?? new string[0]).Select(x=> '"' + x + '"'));
            return this;
        }
        
        public CommandExecutorParameters Build(params string[] directory)
        {
            return new CommandExecutorParameters(this);
        }
    }
}

/*
 
 */
