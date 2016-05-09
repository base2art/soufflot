namespace Base2art.Soufflot.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;

    public abstract class SimpleRenderingRouted : IRenderingRouted
    {
        IPositionedRenderingRouted[] IRenderingRouted.RenderingRoutedItems
        {
            get { return (this.RenderingRoutedItems ?? new IPositionedRenderingRouted[] { }).ToArray(); }
        }

        INonRenderingRouted[] IRenderingRouted.NonRenderingRoutedItems
        {
            get { return (this.NonRenderingRoutedItems ?? new INonRenderingRouted[] { }).ToArray(); }
        }

        protected virtual IEnumerable<INonRenderingRouted> NonRenderingRoutedItems
        {
            get { yield break; }
        }

        protected virtual IEnumerable<IPositionedRenderingRouted> RenderingRoutedItems
        {
            get { yield break; }
        }

        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            return this.ExecuteMain(httpContext, childResults);
        }

        protected abstract IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults);
        
        protected INonRenderingRouted CreateNonRenderingRouted(Action<IHttpContext> childRoutedAction)
        {
            return Routed.CreateNonRenderingRouted(childRoutedAction);
        }
        
        /// <summary>
        /// This method is used for rendering a child View that probably doesn't have a ViewModel.
        /// </summary>
        /// <param name="containerId">The Id of the container where the view will be displayed.</param>
        /// <param name="priority">The order of the view in the container.</param>
        /// <param name="childRoutedFunc">The delegate that should probably return a view result.</param>
        /// <returns>The Routed that will be rendered.</returns>
        protected IPositionedRenderingRouted CreateRenderingRouted(
            int containerId,
            int priority,
            Func<IHttpContext, List<PositionedResult>, IResult> childRoutedFunc)
        {
            return Routed.CreatePositionedRenderingRouted(containerId, priority, childRoutedFunc);
        }
        
        /// <summary>
        /// This method is used for rendering a child Routed that probably does have a ViewModel.
        /// </summary>
        /// <param name="containerId">The Id of the container where the view will be displayed.</param>
        /// <param name="priority">The order of the view in the container.</param>
        /// <param name="renderingRouted">The delegate that should probably return a view result.</param>
        /// <returns>The Routed that will be rendered.</returns>
        protected IPositionedRenderingRouted CreateRenderingRouted(
            int containerId,
            int priority,
            IRenderingRouted renderingRouted)
        {
            return Routed.CreatePositionedRenderingRouted(containerId, priority, renderingRouted);
        }
    }
}
