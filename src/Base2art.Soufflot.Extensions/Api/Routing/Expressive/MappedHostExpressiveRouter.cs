namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Http;

    public class MappedLocalHostExpressiveRouter : ExpressiveRouter
    {
        private readonly string defaultHost;

        private readonly ICurrentHttpContextProvider domainProvider;

        public MappedLocalHostExpressiveRouter(string defaultHost, ICurrentHttpContextProvider domainProvider)
        {
            this.defaultHost = defaultHost;
            this.domainProvider = domainProvider;
        }

        protected override IRoutable<T> ForController<T>()
        {
            var currentDomainProvider = this.domainProvider;
            string backupHost = this.defaultHost;
            if (currentDomainProvider != null)
            {
                backupHost = currentDomainProvider.Current.Request.Host;
            }

            return base.ForController<T>().OnDomain(this.MapRequestHost(backupHost));
        }

        protected override IEnumerable<ExpressiveRouteData<T>> Filter<T>(IHttpRequest request, LinkedList<RouteInfo> routeDatum)
        {
            return this.FilterByParts<T>(
                routeDatum,
                this.MapRequestHost(request.Host),
                request.Path,
                request.Method);
        }

        private string MapRequestHost(string requestHost)
        {
            return requestHost == "localhost" ? this.defaultHost : requestHost;
        }
    }
}