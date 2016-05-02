namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class StringExpressiveRouterRegistration
    {
        private readonly ExpressiveRouter router;

        private readonly string path;

        private string domain;

        private HttpMethod? httpMethod;

        public StringExpressiveRouterRegistration(ExpressiveRouter router, string path)
        {
            this.router = router;
            this.path = path;
        }

        public string Path
        {
            get { return this.path; }
        }

        public string Domain
        {
            get { return this.domain; }
        }

        public HttpMethod? HttpMethod
        {
            get { return this.httpMethod; }
        }

        protected ExpressiveRouter Router
        {
            get { return this.router; }
        }

        public StringExpressiveRouterRegistration OnDomain(string registerDomain)
        {
            this.domain = registerDomain;
            return this;
        }

        public StringExpressiveRouterRegistration WithMethod(HttpMethod method)
        {
            this.httpMethod = method;
            return this;
        }

        public void ToController<T>()
            where T : IRenderingRouted
        {
            var routeData = new RouteInfo
            {
                Method = this.HttpMethod,
                Host = this.Domain,
                Path = this.Path,
                Type = typeof(T)
            };

            var func = this.Create<T>((x, y, z) => x.Execute(y, z));

            var tree = new ExpressiveRouteValidator<T>().ValidateExpression(func);
            routeData.Expression = func;
            routeData.ExpressionTree = tree;
            routeData.WrapperExpression = (x, y, z) => func.Compile().Invoke((T)x, y, z);
            this.Router.Register(routeData);
        }

        public StringExpressiveRouterRegistration<T> To<T>()
            where T : IRenderingRouted
        {
            var i = new StringExpressiveRouterRegistration<T>(this.router, this.Path);
            i.httpMethod = this.httpMethod;
            i.domain = this.domain;
            return i;
        }

        protected Expression<Func<T, IHttpContext, List<PositionedResult>, IResult>> Create<T>(Expression<Func<T, IHttpContext, List<PositionedResult>, IResult>> func)
            where T : IRenderingRouted
        {
            return func;
        }
    }

    public class StringExpressiveRouterRegistration<T> : StringExpressiveRouterRegistration
            where T : IRenderingRouted
    {
        public StringExpressiveRouterRegistration(ExpressiveRouter router, string path)
            : base(router, path)
        {
        }

        public void Method(Expression<Func<T, IHttpContext, List<PositionedResult>, IResult>> func)
        {
            var routeData = new RouteInfo
            {
                Method = this.HttpMethod,
                Host = this.Domain,
                Path = this.Path,
                Type = typeof(T)
            };

            if (func == null)
            {
                func = this.Create<T>((x, y, z) => x.Execute(y, z));
            }

            var tree = new ExpressiveRouteValidator<T>().ValidateExpression(func);
            routeData.Expression = func;
            routeData.ExpressionTree = tree;
            routeData.WrapperExpression = (x, y, z) => func.Compile().Invoke((T)x, y, z);
            this.Router.Register(routeData);
        }
    }
}