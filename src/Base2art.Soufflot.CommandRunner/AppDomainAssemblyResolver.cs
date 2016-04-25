namespace Base2art.Soufflot.CommandRunner
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class AppDomainAssemblyResolver
    {
        private readonly string binPath;

        private readonly string currentDomainBin;

        public AppDomainAssemblyResolver(string binPath, string currentDomainBin)
        {
            this.binPath = binPath;
            this.currentDomainBin = currentDomainBin;
        }

        public Assembly ResolveAssembly(object a, ResolveEventArgs b)
        {
            var name = b.Name;
            if (name.Contains(","))
            {
                name = new string(name.TakeWhile(x=>x != ',').ToArray());
            }

            var dllPath = Path.Combine(this.binPath, name + ".dll");
            if (File.Exists(dllPath))
            {
                return Assembly.LoadFile(dllPath);
            }

            dllPath = Path.Combine(this.currentDomainBin, name + ".dll");
            
            if (File.Exists(dllPath))
            {
                return Assembly.LoadFile(dllPath);
            }

            return null;
        }
    }
}