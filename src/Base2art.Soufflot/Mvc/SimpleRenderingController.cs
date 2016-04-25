namespace Base2art.Soufflot.Mvc
{
	using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;

    public abstract class SimpleRenderingController : IRenderingController
    {
        IPositionedRenderingController[] IRenderingController.RenderingControllers
        {
            get
            {
                return (this.RenderingControllers ?? new IPositionedRenderingController[] { }).ToArray();
            }
        }

        INonRenderingController[] IRenderingController.NonRenderingControllers
        {
            get
            {
                return (this.NonRenderingControllers ?? new INonRenderingController[] { }).ToArray();
            }
        }

        protected virtual IEnumerable<INonRenderingController> NonRenderingControllers
        {
            get { yield break; }
        }

        protected virtual IEnumerable<IPositionedRenderingController> RenderingControllers
        {
            get { yield break; }
        }

        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            return this.ExecuteMain(httpContext, childResults);
        }

        protected abstract IResult ExecuteMain(IHttpContext httpContext, List<PositionedResult> childResults);
        
        protected INonRenderingController CreateNonRenderingController(Action<IHttpContext> childControllerAction)
        {
            return Controller.CreateNonRenderingController(childControllerAction);
        }
        
        /// <summary>
        /// This method is used for rendering a child View that probably doesn't have a ViewModel.
        /// </summary>
        /// <param name="containerId">The Id of the container where the view will be displayed.</param>
        /// <param name="priority">The order of the view in the container.</param>
        /// <param name="childControllerFunc">The delegate that should probably return a view result.</param>
        /// <returns>The controller that will be rendered.</returns>
        protected IPositionedRenderingController CreateRenderingController(
            int containerId,
            int priority,
            Func<IHttpContext, List<PositionedResult>, IResult> childControllerFunc)
        {
            return Controller.CreatePositionedRenderingController(containerId, priority, childControllerFunc);
        }
        
        /// <summary>
        /// This method is used for rendering a child controller that probably does have a ViewModel.
        /// </summary>
        /// <param name="containerId">The Id of the container where the view will be displayed.</param>
        /// <param name="priority">The order of the view in the container.</param>
        /// <param name="renderingController">The delegate that should probably return a view result.</param>
        /// <returns>The controller that will be rendered.</returns>
        protected IPositionedRenderingController CreateRenderingController(
            int containerId,
            int priority,
            IRenderingController renderingController)
        {
            return Controller.CreatePositionedRenderingController(containerId, priority, renderingController);
        }
    }
}