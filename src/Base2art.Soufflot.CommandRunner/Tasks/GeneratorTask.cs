namespace Base2art.Soufflot.CommandRunner.Tasks
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using Base2art.Soufflot.Mvc;

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
            createDirFrom("App\\ViewModels");
            createDirFrom("Conf");
            createDirFrom("Logs");
            createDirFrom("Project");
            createDirFrom("Project\\lib");
            createDirFrom("Public");
            createDirFrom("Tools");
            createDirFrom("Test");
            
            this.WriteAllText(
                Path.Combine(directory, "App\\Views\\Home.fs.html"),
                this.GetResourceClean("Base2art.Soufflot.CommandRunner.Resx.App.Views.Home.fs.html"));
            
            this.WriteAllText(
                Path.Combine(directory, "App\\Controllers\\HomeController.cs"),
                this.GetResourceClean("Base2art.Soufflot.CommandRunner.Resx.App.Controllers.HomeController.cs"));
            
            this.WriteAllText(
                Path.Combine(directory, "Project\\packages.config"),
                this.PackagesXmlDocument().ToString(SaveOptions.None));
            
            this.WriteAllText(
                Path.Combine(directory, "app.csproj"),
                this.GetResourceClean("Base2art.Soufflot.CommandRunner.Resx.base.csproj"));
            
            this.WriteAllText(
                Path.Combine(directory, "app.csproj.DotSettings"),
                this.GetResourceClean("Base2art.Soufflot.CommandRunner.Resx.base.csproj.DotSettings"));
            
            this.WriteAllText(
                Path.Combine(directory, "app.ViewModels.csproj"),
                this.GetResourceClean("Base2art.Soufflot.CommandRunner.Resx.base.ViewModels.csproj"));
            
            this.WriteAllText(
                Path.Combine(directory, "app.ViewModels.csproj.DotSettings"),
                this.GetResourceClean("Base2art.Soufflot.CommandRunner.Resx.base.csproj.DotSettings"));
            
            this.WriteAllText(
                Path.Combine(directory, "app.sln"),
                this.GetResourceClean("Base2art.Soufflot.CommandRunner.Resx.base.sln"));
            
            this.WriteAllText(
                Path.Combine(directory, "Conf\\ApplicationBuilder.cs"),
                this.GetResourceClean("Base2art.Soufflot.CommandRunner.Resx.Conf.ApplicationBuilder.cs"));
            
            this.WriteAllText(
                Path.Combine(directory, "Conf\\CustomRoutes.cs"),
                this.GetResourceClean("Base2art.Soufflot.CommandRunner.Resx.Conf.CustomRoutes.cs"));
            
            this.WriteAllText(
                Path.Combine(directory, "Project\\Views.json"),
                this.GetResourceClean("Base2art.Soufflot.CommandRunner.Resx.Project.Views.json"));
            
            this.WriteAllText(
                Path.Combine(directory, "NuGet.config"),
                this.GetResourceClean("Base2art.Soufflot.CommandRunner.Resx.nuget.config"));
            
            this.WriteAllText(
                Path.Combine(directory, "App.props"),
                this.AppProps());
            
            this.WriteAllText(
                Path.Combine(directory, "App\\Info.cs"),
                "// Copyright (" + DateTimeOffset.UtcNow.Year + ")");
            
            #if DEBUG
            
            var dlls = new Type[]
            {
                typeof(Base2art.Soufflot.IRoute), // "Base2art.Soufflot",
                typeof(Base2art.Soufflot.StringExtender), // "Base2art.Soufflot.Extensions",
                typeof(Base2art.Soufflot.Http.Owin.HttpContext), // "Base2art.Soufflot.Http.Owin",
                typeof(MonketTailContentMapper), // "Base2art.Soufflot.MonkeyTail",
            };
            
            foreach (var dll in dlls)
            {
                var assembly = dll.Assembly;
                var nameContainer = assembly.GetName();
                
                var path = string.Format("Project\\lib\\{0}.{1}\\lib\\net\\", nameContainer.Name, nameContainer.Version);
                createDirFrom(path);
                
                File.Copy(
                    assembly.Location,
                    Path.Combine(directory, path, nameContainer.Name + ".dll"),
                    true);
            }
            
            #endif
            
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
            
            add("Base2art.Soufflot", "1.0.0", "net45");
            add("Base2art.Soufflot.Extensions", "1.0.0", "net45");
            add("Base2art.Soufflot.Http.Owin", "1.0.0", "net45");
            add("Base2art.Soufflot.CommandRunner", "1.0.0", "net45");
            
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
            sb.AppendLine("soufflot:salt = " + this.GetGuids(3));
            sb.AppendLine("soufflot:app-builder-class-name = App.Conf.ApplicationBuilder, App.BASE_NAME");
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
