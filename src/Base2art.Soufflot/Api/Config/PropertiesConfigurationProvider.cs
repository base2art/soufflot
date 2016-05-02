namespace Base2art.Soufflot.Api.Config
{
    using System.IO;

    public class PropertiesConfigurationProvider : IConfigurationProvider
    {
        private readonly ILazy<Properties> properties;

        public PropertiesConfigurationProvider(Stream stream)
        {
            var x = Properties.Load(stream);
            this.properties = new OneTryLazy<Properties>(() => x);
        }

        public PropertiesConfigurationProvider(string fileName)
        {
            this.properties = new OneTryLazy<Properties>(
                () =>
                {
                    if (!File.Exists(fileName))
                    {
                        return new Properties();
                    }

                    using (var fileContent = File.OpenRead(fileName))
                    {
                        return Properties.Load(fileContent);
                    }
                });
        }

        public string GetValue(string key)
        {
            if (this.properties.Value.Contains(key))
            {
                return this.properties.Value[key];
            }

            return null;
        }
    }
}
