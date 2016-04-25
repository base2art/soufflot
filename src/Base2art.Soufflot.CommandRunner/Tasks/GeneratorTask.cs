namespace Base2art.Soufflot.CommandRunner.Tasks
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    public class GeneratorTask : TaskBase<GenerateOptions>
    {
        public GeneratorTask(GenerateOptions opts)
            : base(opts)
        {
        }

        protected override void ExecuteInternal()
        {
            var directory = this.Options.Directory;
            
            if (string.IsNullOrWhiteSpace(directory))
            {
                directory = Environment.CurrentDirectory;
            }
            
            directory = Path.Combine(directory, this.Options.AppName);
            
            if (!Path.IsPathRooted(directory)) 
            {
                directory = Path.Combine(Environment.CurrentDirectory, directory);
            }
            
            Action<String> createDirFrom = s => Directory.CreateDirectory(Path.Combine(directory, s));
            Directory.CreateDirectory(directory);
            
            createDirFrom("App");
            createDirFrom("App\\Controllers");
            createDirFrom("App\\Models");
            createDirFrom("App\\Views");
            createDirFrom("Conf");
            createDirFrom("Logs");
            createDirFrom("Project");
            createDirFrom("Project\\lib");
            createDirFrom("Public");
            createDirFrom("Tools");
            createDirFrom("Test");
            //            createDirFrom(".nuget");
            
            this.WriteAllText(
                Path.Combine(directory, "project\\packages.config"),
                this.PackagesXmlDocument().ToString(SaveOptions.None));
            
            this.WriteAllText(
                Path.Combine(directory, "app.csproj"),
                this.GetResourceClean("Base2art.PlayN.Pack.Generator.Console.Resx.base.csproj"));
            
            this.WriteAllText(
                Path.Combine(directory, "app.csproj.DotSettings"),
                this.GetResourceClean("Base2art.PlayN.Pack.Generator.Console.Resx.base.csproj.DotSettings"));
            
            
            this.WriteAllText(
                Path.Combine(directory, "app.ViewModels.csproj"),
                this.GetResourceClean("Base2art.PlayN.Pack.Generator.Console.Resx.base.ViewModels.csproj"));
            
            this.WriteAllText(
                Path.Combine(directory, "app.ViewModels.csproj.DotSettings"),
                this.GetResourceClean("Base2art.PlayN.Pack.Generator.Console.Resx.base.csproj.DotSettings"));
            
            
            this.WriteAllText(
                Path.Combine(directory, "app.sln"),
                this.GetResourceClean("Base2art.PlayN.Pack.Generator.Console.Resx.base.sln"));
            
            this.WriteAllText(
                Path.Combine(directory, "Conf\\ApplicationBuilder.cs"),
                this.GetResourceClean("Base2art.PlayN.Pack.Generator.Console.Resx.Conf.ApplicationBuilder.cs"));
            
            this.WriteAllText(
                Path.Combine(directory, "Conf\\CustomRoutes.cs"),
                this.GetResourceClean("Base2art.PlayN.Pack.Generator.Console.Resx.Conf.CustomRoutes.cs"));
            
            this.WriteAllText(
                Path.Combine(directory, "Project\\Views.yaml"),
                this.GetResourceClean("Base2art.PlayN.Pack.Generator.Console.Resx.Project.Views.yaml"));
            
            this.WriteAllText(
                Path.Combine(directory, "NuGet.config"),
                this.GetResourceClean("Base2art.PlayN.Pack.Generator.Console.Resx.nuget.config"));
            
            this.WriteAllText(
                Path.Combine(directory, "App.props"),
                this.AppProps());
            
            this.WriteAllText(
                Path.Combine(directory, "App\\Info.cs"),
                "// Copyright (" + DateTimeOffset.UtcNow.Year + ")");
            
            var installTask = new InstallNuGetTask(new InstallNuGetOptions()).InstallNuGet();
            
            new RestoreNuGetTask(new RestoreNuGetOptions { Directory = directory, NuGetPath = installTask }).Execute();
        }
        
        public XElement PackagesXmlDocument()
        {
            var xmlElement = new XElement("packages");
            
            Action<string, string, string> add = (x, y, z) => xmlElement.Add(new XElement("package",
                                                         new XAttribute("id", x),
                                                         new XAttribute("version", y),
                                                         new XAttribute("targetFramework", z)));
            
            add("Microsoft.Owin", "2.1.0", "net45");
            add("Microsoft.Owin.Host.SystemWeb", "2.1.0", "net45");
            
            add("Owin", "1.0", "net45");
            add("Newtonsoft.Json", "6.0.3", "net45");
            
            add("Base2art.Bcl", "1.0.0", "net45");
            
            add("Base2art.PlayN", "1.0.0", "net45");
            add("Base2art.PlayN.Extensions", "1.0.0", "net45");
            add("Base2art.PlayN.Http.Owin", "1.0.0", "net45");
            add("Base2art.PlayN.Pack", "1.0.0", "net45");
            
            add("Base2art.MonkeyTail.Api", "1.0.0", "net45");
            add("Base2art.MonkeyTail.Compiler.Console", "1.0.0", "net45");
            
            return xmlElement;
        }

        private void WriteAllText(string path, string text)
        {
            if (File.Exists(path))
            {
                return;
            }
            
            File.WriteAllText(path, text);
        }
        
        private string GetResourceClean(string resxName)
        {
            return this.Clean(this.GetResource(resxName));
        }

        private string Clean(string value)
        {
            return value.Replace("BASE_NAME", this.Options.AppName);
        }
        
        private string GetResource(string resxName)
        {
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resxName))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        
        private string AppProps()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("# Change Salt For Production");
            sb.AppendLine("playn:salt = " + this.GetGuids(3));
            sb.AppendLine("playn:app-builder-class-name = App.Conf.ApplicationBuilder, App.BASE_NAME");
            return this.Clean(sb.ToString());
        }

        private string GetGuids(int end)
        {
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < end; i++)
            {
                sb.Append(Guid.NewGuid().ToString("N"));
            }
            
            return sb.ToString();
        }
    }
}

