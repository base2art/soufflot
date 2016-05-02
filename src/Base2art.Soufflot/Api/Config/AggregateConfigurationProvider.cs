namespace Base2art.Soufflot.Api.Config
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Linq;

    public class AggregateConfigurationProvider : IConfigurationProvider
    {
        private readonly IEnumerable<IConfigurationProvider> providers;

        public AggregateConfigurationProvider(params IConfigurationProvider[] providers)
        {
            this.providers = providers.Coalesce();
        }

        public string GetValue(string key)
        {
            foreach (var provider in this.providers)
            {
                var value = provider.GetValue(key);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return null;
        }
    }
}
