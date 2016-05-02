namespace Base2art.Soufflot.Api.Routing.Functional
{
    using System;

    using Base2art.Soufflot.Http;

    public interface IRenderingControllerSearchDelegate
    {
        Type FindType(IHttpRequest request);
    }
}
