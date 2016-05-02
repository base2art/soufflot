namespace Base2art.Soufflot.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Base2art.Soufflot.Linq;

    using Base2art.Soufflot.Http;

    public class RoutedExecutionManager
    {
        private readonly IApplication application;

        private readonly IRouter router;

        public RoutedExecutionManager(IApplication application, IRouter router)
        {
            this.application = application;
            this.router = router;
        }

        public IApplication Application
        {
            get
            {
                return this.application;
            }
        }

        public IResult ExecuteRoute(IHttpContext httpContext)
        {
            var findRenderingRoutedType = this.router.FindRenderingRoutedType(httpContext.Request);
            if (findRenderingRoutedType == null)
            {
                return application.OnRouteNotFound(httpContext);
            }

            var findNonRenderingRoutedTypes = this.router.FindNonRenderingRoutedTypes(httpContext.Request);

            IClass<IRenderingRouted> renderingRoutedClass = findRenderingRoutedType.RoutedClass;
            
            if (renderingRoutedClass == null)
            {
                return application.OnRouteNotFound(httpContext);
            }

            IRenderingRouted mainRoutedItem = application.CreateInstance(renderingRoutedClass, application.Mode == ApplicationMode.Prod);
            
            if (mainRoutedItem == null)
            {
                return application.OnRouteNotFound(httpContext);
            }

            INonRenderingRouted[] nonRenderingRoutedItems = findNonRenderingRoutedTypes
                .Where(x => x != null)
                .Select(x => application.CreateInstance(x.RoutedClass, true))
                .ToArray();

            this.ExecuteNonRenderingRoutes(httpContext, nonRenderingRoutedItems);

            return this.ExecuteRenderingRoute(httpContext, mainRoutedItem, findRenderingRoutedType.Expression);
        }

        private IResult ExecuteRenderingRoute(
            IHttpContext httpContext, 
            IRenderingRouted renderingRouted,
            Expression<Func<IRenderingRouted, IHttpContext, List<PositionedResult>, IResult>> expression)
        {
            this.ExecuteNonRenderingRoutes(httpContext, renderingRouted.NonRenderingRoutedItems);

            List<PositionedResult> childResults = new List<PositionedResult>();
            foreach (var subRenderingRoutedItem in renderingRouted.RenderingRoutedItems.Coalesce())
            {
                var subResult = this.ExecuteRenderingRoute(httpContext, subRenderingRoutedItem.RenderingRoutedItem, null);

                var pr = new PositionedResult
                {
                    Container = subRenderingRoutedItem.Container,
                    ContainerPriority = subRenderingRoutedItem.ContainerPriority,
                    Result = new SimpleResult
                             {
                                 Content = subResult.Content
                             },
                };

                childResults.Add(pr);
            }

            if (expression != null)
            {
                return expression.Compile().Invoke(renderingRouted, httpContext, childResults);
            }

            return renderingRouted.Execute(httpContext, childResults);
        }

        private void ExecuteNonRenderingRoutes(
            IHttpContext httpContext,
            INonRenderingRouted[] nonRenderingRoutedItems)
        {
            foreach (var nonRenderingRoutedItem in nonRenderingRoutedItems.Coalesce())
            {
                nonRenderingRoutedItem.Execute(httpContext);
                this.ExecuteNonRenderingRoutes(httpContext, nonRenderingRoutedItem.NonRenderingRoutedItems);
            }
        }
    }
}
