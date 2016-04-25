namespace Base2art.Soufflot.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Base2art.Soufflot.Linq;
    using Base2art.Soufflot.Mvc;

    using Base2art.Soufflot.Http;

    public class ControllerExecutionManager
    {
        private readonly IApplication application;

        private readonly IRouter router;

        public ControllerExecutionManager(IApplication application, IRouter router)
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

        public IResult ExecuteController(IHttpContext httpContext)
        {
            var findRenderingControllerType = this.router.FindRenderingControllerType(httpContext.Request);
            if (findRenderingControllerType == null)
            {
                return application.OnControllerNotFound(httpContext);
            }

            var findNonRenderingControllerTypes = this.router.FindNonRenderingControllerTypes(httpContext.Request);

            IClass<IRenderingController> renderingControllerClass = findRenderingControllerType.ControllerClass;
            
            if (renderingControllerClass == null)
            {
                return application.OnControllerNotFound(httpContext);
            }

            IRenderingController mainController = application.CreateInstance(renderingControllerClass, application.Mode == ApplicationMode.Prod);
            
            if (mainController == null)
            {
                return application.OnControllerNotFound(httpContext);
            }

            INonRenderingController[] nonRenderingControllers = findNonRenderingControllerTypes
                .Where(x => x != null)
                .Select(x => application.CreateInstance(x.ControllerClass, true))
                .ToArray();

            this.ExecuteNonRenderingControllers(httpContext, nonRenderingControllers);

            return this.ExecuteRenderingController(httpContext, mainController, findRenderingControllerType.Expression);
        }

        private IResult ExecuteRenderingController(
            IHttpContext httpContext, 
            IRenderingController renderingController,
            Expression<Func<IRenderingController, IHttpContext, List<PositionedResult>, IResult>> expression)
        {
            this.ExecuteNonRenderingControllers(httpContext, renderingController.NonRenderingControllers);

            List<PositionedResult> childResults = new List<PositionedResult>();
            foreach (var subRenderingController in renderingController.RenderingControllers.Coalesce())
            {
                var subResult = this.ExecuteRenderingController(httpContext, subRenderingController.RenderingController, null);

                var pr = new PositionedResult
                {
                    Container = subRenderingController.Container,
                    ContainerPriority = subRenderingController.ContainerPriority,
                    Result = new SimpleResult
                             {
                                 Content = subResult.Content
                             },
                };

                childResults.Add(pr);
            }

            if (expression != null)
            {
                return expression.Compile().Invoke(renderingController, httpContext, childResults);
            }

            return renderingController.Execute(httpContext, childResults);
        }

        private void ExecuteNonRenderingControllers(
            IHttpContext httpContext,
            INonRenderingController[] nonRenderingControllers)
        {
            foreach (var nonRenderingController in nonRenderingControllers.Coalesce())
            {
                nonRenderingController.Execute(httpContext);
                this.ExecuteNonRenderingControllers(httpContext, nonRenderingController.NonRenderingControllers);
            }
        }
    }
}
