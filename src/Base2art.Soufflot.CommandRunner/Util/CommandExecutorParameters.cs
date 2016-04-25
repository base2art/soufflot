namespace Base2art.Soufflot.CommandRunner.Util
{
    using System;

    public class CommandExecutorParameters
    {
        private readonly string workingDirectory;

        private readonly string executable;

        private readonly string parameters;

        public CommandExecutorParameters(CommandExecutorBuilder commandExecutorBuilder)
        {
            this.workingDirectory = commandExecutorBuilder.WorkingDirectory;
            this.executable = commandExecutorBuilder.Executable;
            this.parameters = commandExecutorBuilder.Parameters;
        }

        public string Executable
        {
            get { return this.executable; }
        }

        public string Parameters
        {
            get { return this.parameters; }
        }

        public string WorkingDirectory
        {
            get { return this.workingDirectory; }
        }
    }
}

