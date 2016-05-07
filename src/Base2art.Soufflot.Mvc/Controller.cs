namespace Base2art.Soufflot.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    
    public static class Routed
    {
        public static INonRenderingRouted CreateNonRenderingRouted(
            Action<IHttpContext> childRoutedAction)
        {
            return new NonRenderingRouted(childRoutedAction);
        }
        
        public static IPositionedRenderingRouted CreatePositionedRenderingRouted(
            int containerId,
            int priority,
            Func<IHttpContext, List<PositionedResult>, IResult> childRoutedFunc)
        {
            return CreatePositionedRenderingRouted(containerId, priority, CreateRenderingRouted(childRoutedFunc));
        }
        
        public static IPositionedRenderingRouted CreatePositionedRenderingRouted(
            int containerId,
            int priority,
            IRenderingRouted renderingRouted)
        {
            return new PositionedRenderingRouted
            {
                RenderingRoutedItem = renderingRouted ?? new NullRenderingRouted(),
                Container = containerId,
                ContainerPriority = priority
            };
        }
        
        public static IRenderingRouted CreateRenderingRouted(
            Func<IHttpContext, List<PositionedResult>, IResult> childRoutedFunc)
        {
            return new RenderingRouted(childRoutedFunc);
        }
        
        private class PositionedRenderingRouted : IPositionedRenderingRouted
        {
            public IRenderingRouted RenderingRoutedItem { get; set; }

            public int Container { get; set; }

            public int ContainerPriority { get; set; }
        }
        
        private class NonRenderingRouted : SimpleNonRenderingRouted
        {
            private readonly Action<IHttpContext> childRoutedAction;

            public NonRenderingRouted(Action<IHttpContext> childRoutedAction)
            {
                this.childRoutedAction = childRoutedAction;
            }
            
            protected override void ExecuteMain(IHttpContext httpContext)
            {
                if (this.childRoutedAction != null)
                {
                    this.childRoutedAction(httpContext);
                }
            }
        }
        
        private class RenderingRouted : SimpleRenderingRouted
        {
            private readonly Func<IHttpContext, List<PositionedResult>, IResult> childRoutedFunc;

            public RenderingRouted(Func<IHttpContext, List<PositionedResult>, IResult> childRoutedFunc)
            {
                this.childRoutedFunc = childRoutedFunc;
            }

            protected override IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults)
            {
                if (this.childRoutedFunc != null)
                {
                    return this.childRoutedFunc(httpContext, childResults);
                }
                
                return httpContext.NoContent();
            }
        }
    }
}
