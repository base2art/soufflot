namespace Base2art.Soufflot.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;

    public abstract class SimpleNonRenderingRouted : INonRenderingRouted
    {
        INonRenderingRouted[] INonRenderingRouted.NonRenderingRoutedItems
        {
            get
            {
                return (this.NonRenderingRoutedItems ?? new INonRenderingRouted[] { }).ToArray();
            }
        }

        protected virtual IEnumerable<INonRenderingRouted> NonRenderingRoutedItems
        {
            get { yield break; }
        }

        public void Execute(IHttpContext httpContext)
        {
            this.ExecuteMain(httpContext);
        }

        protected abstract void ExecuteMain(IHttpContext httpContext);
        
        protected INonRenderingRouted CreateNonRenderingRouted(Action<IHttpContext> childRoutedAction)
        {
            return Routed.CreateNonRenderingRouted(childRoutedAction);
        }
    }
}
