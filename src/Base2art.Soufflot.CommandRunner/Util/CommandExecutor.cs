namespace Base2art.Soufflot.CommandRunner.Util
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    
    public static class CommandExecutor
    {
        private const int CodePage = 1;

        private const int MaxDefaultCharacters = 2;

        private const int MaxLeadingBytes = 12;

        private const int MaxPathLength = 260;
        
        private static readonly Lazy<Encoding> Encoding = new Lazy<Encoding>(InitEncoding);

        public static CommandExecutorBuilder Builder()
        {
            return new CommandExecutorBuilder();
        }
        
        public static int ExecuteCommand(this CommandExecutorParameters parameters)
        {
            return ExecuteCommand(
                parameters.Executable,
                parameters.Parameters,
                parameters.WorkingDirectory, 
                Console.Out.WriteLine,
                Console.Error.WriteLine);
        }
        
        public static int ExecuteCommand(string executable, string arguments, string workingDirectory, Action<string> output, Action<string> error)
        {
            int exitCode;
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = executable;
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.WorkingDirectory = workingDirectory;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.StandardOutputEncoding = Encoding.Value;
                    process.StartInfo.StandardErrorEncoding = Encoding.Value;
                    using (var outputWaitHandle = new AutoResetEvent(false))
                    {
                        using (var errorWaitHandle = new AutoResetEvent(false))
                        {
                            process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
                            {
                                if (e.Data == null)
                                {
                                    outputWaitHandle.Set();
                                    return;
                                }
                                
                                output(e.Data);
                            };
                            
                            process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e)
                            {
                                if (e.Data == null)
                                {
                                    errorWaitHandle.Set();
                                    return;
                                }
                                
                                error(e.Data);
                            };
                            
                            process.Start();
                            process.BeginOutputReadLine();
                            process.BeginErrorReadLine();
                            process.WaitForExit();
                            outputWaitHandle.WaitOne();
                            errorWaitHandle.WaitOne();
                            exitCode = process.ExitCode;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = string.Format(
                                  CultureInfo.InvariantCulture,
                                  "Error when attempting to execute {0}: {1}",
                                  executable,
                                  ex.Message);
                
                throw new Exception(message, ex);
            }
            
            return exitCode;
        }

        private static System.Text.Encoding InitEncoding()
        {
            try
            {
                NativeMethods.CPINFOEX info;
                return NativeMethods.GetCPInfoEx(1, 0, out info)
                    ? System.Text.Encoding.GetEncoding(info.CodePage)
                    : System.Text.Encoding.GetEncoding(850);
            }
            catch (Exception)
            {
                return System.Text.Encoding.UTF8;
            }
        }
    }
}
