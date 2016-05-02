namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Base2art.Collections;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public static class ExpressiveRouterExtender
    {
        public static void RegisterRoute<TController>(
            this IExpressiveRouter router,
            HttpMethod? method,
            string domain,
            string path)
            where TController : IRenderingRouted
        {
            if (method.HasValue)
            {
                router.Register(path).OnDomain(domain).WithMethod(method.Value).ToController<TController>();
            }
            else
            {
                router.Register(path).OnDomain(domain).ToController<TController>();
            }
        }

        public static void RegisterRoute<TController>(
            this IExpressiveRouter router,
            HttpMethod? method,
            string domain,
            string path,
            Expression<Func<TController, IHttpContext, List<PositionedResult>, IResult>> func)
            where TController : IRenderingRouted
        {
            if (method.HasValue)
            {
                router.Register(path).OnDomain(domain).WithMethod(method.Value)
                    .To<TController>()
                    .Method(func);
            }
            else
            {
                router.Register(path)
                    .OnDomain(domain)
                    .To<TController>()
                    .Method(func);
            }
        }

        // This method is only useful if you are in a single domain Application
        public static IRoute FindRoute<TController>(this IExpressiveReverseRouter router)
            where TController : IRenderingRouted
        {
            return CoalesceRoute(router.For<TController>());
        }

        // This method is only useful if you are in a single domain Application
        public static IRoute FindRouteWith<TController>(this IExpressiveReverseRouter router, params object[] objects)
            where TController : IRenderingRouted
        {
            return router.FindNamedRouteWith<TController>("Execute", objects);
        }

        // This method is only useful if you are in a single domain Application
        public static IRoute FindNamedRoute<TController>(this IExpressiveReverseRouter router, string name) 
            where TController : IRenderingRouted
        {
            return router.FindNamedRouteWith<TController>(name, new object[0]);
        }

        // This method is only useful if you are in a single domain Application
        public static IRoute FindNamedRouteWith<TController>(this IExpressiveReverseRouter router, string name, params object[] objects) where TController : IRenderingRouted
        {
            var func = Create<TController>((x, y, z) => x.Execute(y, z));

            var mce = (MethodCallExpression)func.Body;

            var arguments = mce.Arguments.ToList();
            var argumentTypes = mce.Arguments.Select(x => x.Type).ToList();
            objects.Select(x => x.GetType()).ForAll(argumentTypes.Add);
            objects.Select(Expression.Constant).ForAll(arguments.Add);

            var controllerType = typeof(TController);
            var method = controllerType.GetMethod(name, argumentTypes.ToArray());

            if (method == null)
            {
                return null;
            }

            var call = Expression.Call(mce.Object, method, arguments);
            var newCall = Expression.Lambda(call, func.Parameters);

            var result = router.For<TController>()
                .With((Expression<Func<TController, IHttpContext, List<PositionedResult>, IResult>>)newCall);

            return CoalesceRoute(result);
        }

        private static Expression<Func<T, IHttpContext, List<PositionedResult>, IResult>> Create<T>(Expression<Func<T, IHttpContext, List<PositionedResult>, IResult>> func)
            where T : IRenderingRouted
        {
            return func;
        }

        private static IRoute CoalesceRoute<TController>(IRoutable<TController> route) where TController : IRenderingRouted
        {
            if (!route.HasValue)
            {
                return null;
            }

            return route;
        }
    }
}
