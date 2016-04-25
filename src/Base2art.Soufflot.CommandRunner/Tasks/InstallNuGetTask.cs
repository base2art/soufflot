namespace Base2art.Soufflot.CommandRunner.Tasks
{
    using System;
    using System.IO;
    using System.Net;

    public class InstallNuGetTask : TaskBase<InstallNuGetOptions>
    {
        public InstallNuGetTask(InstallNuGetOptions opts)
            : base(opts)
        {
        }

        protected override void ExecuteInternal()
        {
            this.DownloadNuGet();
        }
        
        public string InstallNuGet()
        {
            return this.DownloadNuGet();
        }

        private string DownloadNuGet()
        {
            string nugetCommandLineDownloadPath = "http://www.nuget.org/api/v2/package/Nuget.CommandLine";
            var tempDir = System.IO.Path.GetTempPath();
            var tempFile = Path.Combine(tempDir, "nuget.zip");
            var nugetDir = Path.Combine(tempDir, "nuget");
            
            if (!File.Exists(tempFile))
            {
                using (var webClient = new WebClient())
                {
                    webClient.DownloadFile(nugetCommandLineDownloadPath, tempFile);
                }
            }
            
            var nugetDest = Path.Combine(nugetDir, "tools\\nuget.exe");
            if (!File.Exists(nugetDest))
            {
                if (Directory.Exists(nugetDir))
                {
                    Directory.Delete(nugetDir, true);
                }
                
                Directory.CreateDirectory(nugetDir);
                System.IO.Compression.ZipFile.ExtractToDirectory(tempFile, nugetDir);
            }
            
            return nugetDest;
        }
    }
}
