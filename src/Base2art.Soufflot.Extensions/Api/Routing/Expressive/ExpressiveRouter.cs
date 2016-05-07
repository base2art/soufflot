namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Base2art.Converters;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class ExpressiveRouter : IExpressiveRouteManager
    {
        private readonly LinkedList<RouteInfo> renderingControllers = new LinkedList<RouteInfo>();

        private readonly LinkedList<RouteInfo> nonRenderingControllers = new LinkedList<RouteInfo>();

        private readonly IDictionary<Type, IObjectParserBase> objectConversionMappers = new Dictionary<Type, IObjectParserBase>();

        public ExpressiveRouter()
            : this(false)
        {
        }

        public ExpressiveRouter(bool supressAddingDefaultParsers)
        {
            if (!supressAddingDefaultParsers)
            {
                this.AddTypeConverter(new BooleanParser());
                this.AddTypeConverter(new ByteParser());
                this.AddTypeConverter(new GuidParser());
                this.AddTypeConverter(new Int16Parser());
                this.AddTypeConverter(new Int32Parser());
                this.AddTypeConverter(new Int64Parser());
                this.AddTypeConverter(new StringParser());
                this.AddTypeConverter(new SByteParser());
                this.AddTypeConverter(new MailAddressParser());
            }
        }

        public void AddTypeConverter<T>(IObjectParser<T> parser)
        {
            objectConversionMappers[typeof(T)] = parser;
        }

        #region Routing

        public IRouteData<IRenderingRouted> FindRenderingControllerType(IHttpRequest request)
        {
            var routeDatum = this.RenderingControllersRouteData;
            return this.Filter<IRenderingRouted>(request, routeDatum).FirstOrDefault();
        }

        public IEnumerable<IRouteData<INonRenderingRouted>> FindNonRenderingControllerTypes(IHttpRequest request)
        {
            var routeDatum = this.NonRenderingControllersRouteData;
            return this.Filter<INonRenderingRouted>(request, routeDatum);
        }

        #endregion

        #region ReverseRouting

        public IRoutable<T> For<T>() where T : IRenderingRouted
        {
            return this.ForController<T>();
        }

        protected virtual IRoutable<T> ForController<T>()
            where T : IRenderingRouted
        {
            return new Routable<T>(this.RenderingControllersRouteData);
        }

        #endregion

        #region Register

        public StringExpressiveRouterRegistration Register(string path)
        {
            return new StringExpressiveRouterRegistration(this, path);
        }

        public RegexExpressiveRouterRegistration Register(Regex pathMatcher, string reversingPathFormat)
        {
            return new RegexExpressiveRouterRegistration(this, pathMatcher, reversingPathFormat, this.objectConversionMappers);
        }

        // Please be careful when calling this method
        public void Register(RouteInfo routeInfo)
        {
            this.RenderingControllersRouteData.AddLast(routeInfo);
        }

        public void RegisterNonRenderingRoute<T>(HttpMethod? method, string hostName, string path)
                    where T : INonRenderingRouted
        {
            this.RegisterNonRendering<T>(method, hostName, path);
        }

        protected virtual RouteInfo RegisterNonRendering<T>(HttpMethod? method, string hostName, string path)
            where T : INonRenderingRouted
        {
            var routeData = new RouteInfo { Method = method, Host = hostName, Path = path, Type = typeof(T) };
            this.NonRenderingControllersRouteData.AddLast(routeData);
            return routeData;
        }

        #endregion

        protected LinkedList<RouteInfo> RenderingControllersRouteData
        {
            get { return this.renderingControllers; }
        }

        protected LinkedList<RouteInfo> NonRenderingControllersRouteData
        {
            get { return this.nonRenderingControllers; }
        }

        protected virtual IEnumerable<ExpressiveRouteData<T>> Filter<T>(IHttpRequest request, LinkedList<RouteInfo> routeDatum)
        {
            return this.FilterByParts<T>(routeDatum, request.Host, request.Path, request.Method);
        }

        protected IEnumerable<ExpressiveRouteData<T>> FilterByParts<T>(
            LinkedList<RouteInfo> routeDatum,
            string requestHost,
            string requestPath,
            HttpMethod requestMethod)
        {
            foreach (var routeData in routeDatum)
            {
                if (!ExpressiveRouteMatcher.IsMatch(requestHost, routeData.Host, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (routeData.PathMatcher == null)
                {
                    if (!ExpressiveRouteMatcher.IsMatch(requestPath, routeData.Path, StringComparison.Ordinal))
                    {
                        continue;
                    }
                }

                if (routeData.PathMatcher != null)
                {
                    if (!ExpressiveRouteMatcher.IsMatchRegex(requestPath, routeData.PathMatcher))
                    {
                        continue;
                    }
                }

                if (!ExpressiveRouteMatcher.IsMatch(requestMethod, routeData.Method))
                {
                    continue;
                }

                yield return new ExpressiveRouteData<T>(routeData.Type.GetClass().As<T>(), routeData.WrapperExpression);
            }
        }
    }
}

/*

        private object[] DeriveParameters(RouteExpressionParameter[] parameters, object reverseRoutingObject)
        {
            List<object> objs = new List<object>(parameters.Length);
            var type = reverseRoutingObject.GetType();
            var props = type.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);

            foreach (var routeExpressionParameter in parameters)
            {
                var constantRouteExpressionParameter = routeExpressionParameter as ConstantRouteExpressionParameter;
                if (constantRouteExpressionParameter != null)
                {
                    objs.Add(constantRouteExpressionParameter.Value);
                    continue;
                }

                var functionalRouteExpressionParameter = (FunctionalRouteExpressionParameter)routeExpressionParameter;
                var name = functionalRouteExpressionParameter.Name;
                var prop = props.FirstOrDefault(x => x.Name == name);
                if (prop == null)
                {
                    // To prevent having a match you must return a null array.
                    return new object[0];
                }

                objs.Add(prop.GetValue(reverseRoutingObject, null));
            }

            return objs.ToArray();
        }

        public IRoute FindRouteWith<T>(params object[] objects) where T : IRenderingController
        {
            return this.FindNamedRouteWith<T>(ExecutionMethod, objects);
        }

        public IRoute FindNamedRoute<T>(string name) where T : IRenderingController
        {
            return this.FindNamedRouteWith<T>(name, new object[0]);
        }

        public IRoute FindNamedDynamicRouteWith<T>(string name, object reverseRoutingObject)
            where T : IRenderingController
        {
            // ReSharper disable once ReplaceWithSingleCallToFirstOrDefault
            var routeData = this.RenderingControllersRouteData
                .Where(x => x.Type == typeof(T))
                .Where(x => x.Host == "HOST")
                .Where(x => x.ExpressionTree.MethodName == name)
                .Where(x => ExpressiveRouteMatcher.IsMatch(x.ExpressionTree.Parameters, this.DeriveParameters(x.ExpressionTree.Parameters, reverseRoutingObject)))
                .FirstOrDefault();

            if (routeData == null)
            {
                return null;
            }

            var pathMatcher = routeData.PathMatcher;
            if (pathMatcher != null)
            {
                return new Route(routeData.Path.Inject(reverseRoutingObject));
            }

            return new Route(routeData.Path);
        }

        public IRoute FindNamedRouteWith<T>(string name, params object[] objects)
            where T : IRenderingController
        {
            // ReSharper disable once ReplaceWithSingleCallToFirstOrDefault
            var routeData = this.RenderingControllersRouteData
                .Where(x => x.Type == typeof(T))
                .Where(x => x.ExpressionTree.MethodName == name)
                .Where(x => ExpressiveRouteMatcher.IsMatch(x.ExpressionTree.Parameters, objects))
                .FirstOrDefault();

            if (routeData == null)
            {
                return null;
            }

            var pathMatcher = routeData.PathMatcher;
            if (pathMatcher != null)
            {
                throw new InvalidOperationException("If you are routing to a complex Route (using regex), you must use the other API. (FindNamedDynamicRouteWith)");
            }

            return new Route(routeData.Path);
        }
        */
