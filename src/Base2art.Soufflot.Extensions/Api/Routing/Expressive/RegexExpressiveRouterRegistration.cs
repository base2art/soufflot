namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    using Base2art.Converters;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class RegexExpressiveRouterRegistration
    {
        private readonly ExpressiveRouter router;

        private readonly Regex pathMatcher;

        private readonly string reversingPathFormat;

        private string domain;

        private HttpMethod httpMethod;

        private readonly IDictionary<Type, IObjectParserBase> objectConverters;

        public RegexExpressiveRouterRegistration(
            ExpressiveRouter router,
            Regex pathMatcher,
            string reversingPathFormat, 
            IDictionary<Type, IObjectParserBase> objectConverters)
        {
            this.router = router;
            this.pathMatcher = pathMatcher;
            this.reversingPathFormat = reversingPathFormat;
            this.objectConverters = objectConverters;
        }

        public Regex PathMatcher
        {
            get { return this.pathMatcher; }
        }

        public string ReversingPathFormat
        {
            get { return this.reversingPathFormat; }
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

        public RegexExpressiveRouterRegistration OnDomain(string registerDomain)
        {
            this.domain = registerDomain;
            return this;
        }

        public RegexExpressiveRouterRegistration WithMethod(HttpMethod method)
        {
            this.httpMethod = method;
            return this;
        }

        public RegexExpressiveRouterRegistration<T> To<T>()
            where T : IRenderingController
        {
            var i = new RegexExpressiveRouterRegistration<T>(
                this.router,
                this.PathMatcher,
                this.ReversingPathFormat,
                this.objectConverters);
            i.httpMethod = this.httpMethod;
            i.domain = this.domain;
            return i;
        }
    }


    public class RegexExpressiveRouterRegistration<T> : RegexExpressiveRouterRegistration
            where T : IRenderingController
    {
        private readonly IDictionary<Type, IObjectParserBase> parsers;

        public RegexExpressiveRouterRegistration(
            ExpressiveRouter router,
            Regex pathMatcher,
            string reversingPathFormat,
            IDictionary<Type, IObjectParserBase> parsers)
            : base(router, pathMatcher, reversingPathFormat, parsers)
        {
            this.parsers = parsers;
        }

        public void Method<TInput>(Expression<Func<T, IHttpContext, List<PositionedResult>, TInput, IResult>> func)
        {
            var routeData = new RouteInfo
            {
                Method = this.HttpMethod,
                Host = this.Domain,
                PathMatcher = this.PathMatcher,
                Type = typeof(T)
            };

            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            IObjectParser<TInput> parser = null;
            var conversionType = typeof(TInput);
            if (this.parsers.ContainsKey(conversionType))
            {
                parser = (IObjectParser<TInput>)this.parsers[conversionType];
            }

            if (parser == null)
            {
                var message =
                    string.Format(
                        "You will have no way to perform the conversion from type of string to '{0}'",
                        conversionType);
                throw new ArgumentOutOfRangeException("func", message);
            }

            var tree = new ExpressiveRouteValidator1<T>(this.PathMatcher).ValidateExpression(func);
            routeData.Expression = func;
            routeData.ExpressionTree = tree;
            routeData.Path = this.ReversingPathFormat;
            routeData.WrapperExpression = (x, y, z) => func.Compile().Invoke((T)x, y, z, this.CreateParam(y, tree.InputParameters.First(), parser));
            this.Router.Register(routeData);
        }

        private TInput CreateParam<TInput>(IHttpContext httpContext, FunctionalRouteExpressionParameter parameter, IObjectParser<TInput> parser)
        {
            string value = null;
            var match = this.PathMatcher.Match(httpContext.Request.Path);
            if (match.Success)
            {
                var matchGroup = match.Groups[parameter.Name];
                if (matchGroup != null && matchGroup.Success)
                {
                    value = matchGroup.Value;
                }
            }

            return parser.ParseOrDefault(value);
        }
    }
}