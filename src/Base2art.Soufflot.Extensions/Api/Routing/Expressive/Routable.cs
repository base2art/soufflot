namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class Routable<T> : IRoutable<T>
        where T : IRenderingRouted
    {
        private const string ExecutionMethod = "Execute";

        private readonly LinkedList<RouteInfo> renderingControllersRouteData;

        private string host;

        private RouteExpressionTree tree = new RouteExpressionTree(
            ExecutionMethod,
            new FunctionalRouteExpressionParameter[0],
            new RouteExpressionParameter[0]);

        public Routable(LinkedList<RouteInfo> renderingControllersRouteData)
        {
            this.renderingControllersRouteData = renderingControllersRouteData;
        }

        protected LinkedList<RouteInfo> RenderingControllersRouteData
        {
            get { return this.renderingControllersRouteData; }
        }

        public bool HasValue
        {
            get
            {
                return this.Explode() != null;
            }
        }

        public string Explode()
        {
            var treeContants =
                this.tree.Parameters.Cast<ConstantRouteExpressionParameter>().Select(y => y.Value).ToArray();

            IEnumerable<RouteInfo> routeDatum = this.RenderingControllersRouteData;

            routeDatum =
                routeDatum.Where(
                    x => ExpressiveRouteMatcher.IsMatch(this.host, x.Host, StringComparison.OrdinalIgnoreCase));
            
            // ReSharper disable once ReplaceWithSingleCallToFirstOrDefault
            var routeData = routeDatum
                .Where(x => x.Type == typeof(T))
                .Where(x => x.ExpressionTree.MethodName == this.tree.MethodName)
                .Where(x => ExpressiveRouteMatcher.IsMatch(x.ExpressionTree.Parameters, treeContants))
                .FirstOrDefault();

            if (routeData == null)
            {
                return null;
            }

            var pathMatcher = routeData.PathMatcher;
            if (pathMatcher != null)
            {
                var reverseRoutingObject = new Dictionary<string, object>();
                foreach (var inputParameter in routeData.ExpressionTree.InputParameters)
                {
                    var inputParamName = inputParameter.Name;
                    int i = 0;
                    foreach (var parameter in routeData.ExpressionTree.Parameters)
                    {
                        var funcParm = parameter as FunctionalRouteExpressionParameter;
                        if (funcParm != null)
                        {
                            if (funcParm.Name == inputParamName)
                            {
                                break;
                            }
                        }

                        i++;
                    }

                    reverseRoutingObject[inputParamName] = treeContants[i];
                }

                return new Route(routeData.Path.Inject(reverseRoutingObject)).Explode();
                //throw new InvalidOperationException("If you are routing to a complex Route (using regex), you must use the other API. (FindNamedDynamicRouteWith)");
            }

            return new Route(routeData.Path).Explode();
        }

        public IRoutable<T> With<TInput1>(Expression<Func<T, IHttpContext, List<PositionedResult>, TInput1, IResult>> expr)
        {
            this.tree = new ExpressiveRouteValidator<T>().ValidateNonFunctionalExpression(expr);
            return this;
        }

        public IRoutable<T> OnDomain(string host)
        {
            this.host = host;
            return this;
        }

        public IRoutable<T> With(Expression<Func<T, IHttpContext, List<PositionedResult>, IResult>> expr)
        {
            this.tree = new ExpressiveRouteValidator<T>().ValidateExpression(expr);
            return this;
        }

        string IRoute.ToString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            return this.Explode();
        }
    }
}
