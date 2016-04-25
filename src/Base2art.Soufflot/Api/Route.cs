namespace Base2art.Soufflot.Api
{
    using System;

    using Base2art.Soufflot.Http;

    public class Route : IRoute
    {
        private readonly string host;

        private readonly string path;

        private readonly HttpMethod? method;

        public Route(string path)
        {
            this.path = path;
        }

        public Route(HttpMethod? method, string host, string path)
        {
            this.method = method;
            this.host = host;
            this.path = path;
        }

        public string Explode()
        {
            if (string.IsNullOrWhiteSpace(this.host))
            {
                return this.path;
            }

            var builder = new UriBuilder("http://localhost/");
            builder.Path = this.path;
            builder.Host = this.host;
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.Explode();
        }
    }
}