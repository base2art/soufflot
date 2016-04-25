
namespace Base2art.Soufflot.CommandRunner.Tasks
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    
    public class UpdateNugetConfigTask : TaskBase<UpdateNugetConfigOptions>
    {
        public UpdateNugetConfigTask(UpdateNugetConfigOptions opts)
            : base(opts)
        {
        }

        protected override void ExecuteInternal()
        {
            var roamingDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            
            var nuGetConfig = Path.Combine(roamingDir, "NuGet\\NuGet.Config");
            
            if (!File.Exists(nuGetConfig))
            {
                File.WriteAllText(nuGetConfig, "<configuration />");
            }
            
            XDocument configDoc = XDocument.Load(nuGetConfig);
            
            
            var packageSources = configDoc.Root
                .Elements("packageSources")
                .FirstOrDefault();
            
            if (packageSources == null)
            {
                packageSources = new XElement("packageSources");
                configDoc.Root.Add(new XElement(packageSources));
            }
            
            Action<string, string> addSource = (name, value)=>
            {
                var item = packageSources.Elements("add")
                    .Select(y => y.Attribute(name))
                    .FirstOrDefault(y => y != null && y.Value == name);
                
                if (item == null)
                {
                    packageSources.Add(new XElement("add",
                                                    new XAttribute("key", name),
                                                    new XAttribute("value", value)));
                }
            };
            
            addSource("Base2Art", "https://nuget.base2art.com/api/v2/");
            addSource("nuget.org", "https://www.nuget.org/api/v2/");
        }
    }
}

/*
 

function update-Nuget-config() {
  
  
  

  $addEl = $packageSource.selectSingleNode("add[@key='Base2Art']")
  
  if ($addEl -eq $null) {
    $addEl = $configDoc.CreateElement("add")
    $addEl.SetAttribute("key", "Base2Art")
    $addEl.SetAttribute("value", $nugetRepoLocalDir)
    $packageSource.appendChild($addEl)
  }


  $addEl = $packageSource.selectSingleNode("add[@key='nuget.org']")
  
  if ($addEl -eq $null) {
    $addEl = $configDoc.CreateElement("add")
    $addEl.SetAttribute("key", "nuget.org")
    $addEl.SetAttribute("value", "https://www.nuget.org/api/v2/")
    $packageSource.appendChild($addEl)
  }
  

  $configDoc.Save($nuGetConfig)
  return $null
}



  $nugetRepoLocalDir = "$Home\nugetserver\Packages"
  if (-Not (Test-Path $nugetRepoLocalDir) ) {
    $nugetImportScript = "git clone git@bitbucket.org:base2art/base2art.localnugetserver.git nugetserver"
    return "You must run '$nugetImportScript' in '$Home'"
  }
 */