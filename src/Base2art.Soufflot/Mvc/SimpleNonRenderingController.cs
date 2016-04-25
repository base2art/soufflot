namespace Base2art.Soufflot.Mvc
{
	using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.Soufflot.Http;

    public abstract class SimpleNonRenderingController : INonRenderingController
    {
        INonRenderingController[] INonRenderingController.NonRenderingControllers
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

        public void Execute(IHttpContext httpContext)
        {
            this.ExecuteMain(httpContext);
        }

        protected abstract void ExecuteMain(IHttpContext httpContext);
        
        protected INonRenderingController CreateNonRenderingController(Action<IHttpContext> childControllerAction)
        {
            return Controller.CreateNonRenderingController(childControllerAction);
        }
    }
}