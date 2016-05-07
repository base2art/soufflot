namespace Base2art.Soufflot.Mvc
{
    using Base2art.Soufflot.Api;
    public class NullNonRenderingRouted : INonRenderingRouted
    {
        void INonRenderingRouted.Execute(Base2art.Soufflot.Http.IHttpContext httpContext)
        {
        }

        INonRenderingRouted[] INonRenderingRouted.NonRenderingRoutedItems
        {
            get
            {
                return new INonRenderingRouted[0];
            }
        }
    }
}
