namespace Base2art.Soufflot.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    
    public static class Controller
    {
        public static INonRenderingController CreateNonRenderingController(
            Action<IHttpContext> childControllerAction)
        {
            return new NonRenderingController(childControllerAction);
        }
        
        public static IPositionedRenderingController CreatePositionedRenderingController(
            int containerId,
            int priority,
            Func<IHttpContext, List<PositionedResult>, IResult> childControllerFunc)
        {
            return CreatePositionedRenderingController(containerId, priority, CreateRenderingController(childControllerFunc));
        }
        
        public static IPositionedRenderingController CreatePositionedRenderingController(
            int containerId,
            int priority,
            IRenderingController renderingController)
        {
            return new PositionedRenderingController {
                RenderingController = renderingController ?? new NullRenderingController(),
                Container = containerId,
                ContainerPriority = priority
            };
        }
        
        public static IRenderingController CreateRenderingController(
            Func<IHttpContext, List<PositionedResult>, IResult> childControllerFunc)
        {
            return new RenderingController(childControllerFunc);
        }
        
        private class PositionedRenderingController : IPositionedRenderingController
        {
            public IRenderingController RenderingController { get; set; }

            public int Container { get; set; }

            public int ContainerPriority { get; set; }
        }
        
        private class NonRenderingController : SimpleNonRenderingController
        {
            private readonly Action<IHttpContext> childControllerAction;

            public NonRenderingController(Action<IHttpContext> childControllerAction)
            {
                this.childControllerAction = childControllerAction;
            }
			
            protected override void ExecuteMain(IHttpContext httpContext)
            {
                if (this.childControllerAction != null)
                {
                    this.childControllerAction(httpContext);
                }
            }
        }
        
        private class RenderingController : SimpleRenderingController
        {
            private readonly Func<IHttpContext, List<PositionedResult>, IResult> childControllerFunc;

            public RenderingController(Func<IHttpContext, List<PositionedResult>, IResult> childControllerFunc)
            {
                this.childControllerFunc = childControllerFunc;
            }

            protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
            {
                if (this.childControllerFunc != null)
                {
                    return this.childControllerFunc(httpContext, childResults);
                }
                
                return httpContext.NoContent();
            }
        }
    }
}
