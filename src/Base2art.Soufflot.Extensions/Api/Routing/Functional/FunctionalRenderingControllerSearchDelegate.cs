namespace Base2art.Soufflot.Api.Routing.Functional
{
    using System;

    using Base2art.Soufflot.Http;

    public class FunctionalRenderingControllerSearchDelegate : IRenderingControllerSearchDelegate
    {
        private readonly Func<IHttpRequest, Type> mapperFunc;

        public FunctionalRenderingControllerSearchDelegate(Func<IHttpRequest, Type> mapperFunc)
        {
            this.mapperFunc = mapperFunc;
        }

        public Type FindType(IHttpRequest request)
        {
            var func = this.mapperFunc;
            if (func != null)
            {
                return func(request);
            }

            return null;
        }
    }
}