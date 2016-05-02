namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System.Text.RegularExpressions;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public interface IExpressiveRouter : IRouter
    {
        void RegisterNonRenderingRoute<T>(
            HttpMethod? method,
            string hostName,
            string path)
            where T : INonRenderingRouted;

        StringExpressiveRouterRegistration Register(string path);

        RegexExpressiveRouterRegistration Register(Regex pathMatcher, string reversingPathFormat);
    }
}


//        void RegisterRoute<T>(
//            HttpMethod? method,
//            string hostName,
//            string path)
//            where T : IRenderingController;
//
//        void RegisterRoute<T>(
//            HttpMethod? method,
//            string hostName,
//            string path,
//            Expression<Func<T, IHttpContext, List<PositionedResult>, IResult>> func)
//            where T : IRenderingController;
//
//        void RegisterRoute<T, TInput>(
//            HttpMethod? method,
//            string hostName,
//            Regex pathMatcher,
//            Expression<Func<T, IHttpContext, List<PositionedResult>, TInput, IResult>> func)
        //            where T : IRenderingController;



//        void RegisterRoute<T>(
//            HttpMethod? method,
//            string hostName,
//            string path,
//            Expression<Func<T, IHttpContext, List<PositionedResult>, IResult>> func)
//            where T : IRenderingController;
//
//        Type FindControllerType(IHttpRequest request);

//
//        void RegisterRouteMatchingUrl<T>(
//            string method, 
//            string hostName, 
//            string path,
//            Expression<Func<T, IHttpContext, List<PositionedResult>, IResult>> func)
//            where T : IRenderingController;


//        IRoute FindRoute<T>(Expression<Func<T, IHttpContext, List<PositionedResult>, IResult>> func)
//            where T : IRenderingController;