namespace Base2art.Soufflot.CommandRunner.Tasks
{
    using System;

    public abstract class TaskBase<TOptions>
    {
        private readonly TOptions options;

        protected TaskBase(TOptions options)
        {
            this.options = options;
        }

        public TOptions Options
        {
            get { return this.options; }
        }
        
        public int Execute()
        {
            try 
            {
                return this.ExecuteInternalWithExitCode();
            } 
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return -1;
            }
        }

        protected virtual int ExecuteInternalWithExitCode()
        {
            this.ExecuteInternal();
            return 0;
        }
        
        protected abstract void ExecuteInternal();
    }
}
