namespace Base2art.Soufflot.Http.Owin
{
    using System;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Diagnostics;

    using Microsoft.Owin;

    public static class Context
    {
        public static IResult ProcessRequest(
            this IOwinContext context,
            RoutedExecutionManager manager,
            string salt,
            ILogger logger)
        {
            return context.ProcessRequest(
                manager,
                manager.Application.CreateInstance<IHttpUserLookup>(Class.GetClass<IHttpUserLookup>(), true),
                salt,
                logger);
        }
        
        public  static IResult ProcessRequest(
            this IOwinContext context,
            RoutedExecutionManager manager,
            IHttpUserLookup instance,
            string salt,
            ILogger logger)
        {
            var httpContext = new HttpContext(
                manager.Application,
                logger,
                instance,
                context,
                new HttpContextSettings { ApplicationSaltSettings = salt });

            CurrentHttpContextProvider.ThreadSpecificHttpContext = httpContext;
            httpContext.Unpack();

            logger.Log(
                string.Format("{0} {1}: {2}", context.Request.Scheme, context.Request.Method, context.Request.Path),
                LogLevels.ApplicationInfo);

            IResult result;
            try
            {
                result = manager.ExecuteRoute(httpContext);
            }
            catch (Exception e)
            {
                //                var st = new System.Diagnostics.StackTrace(e, true);
                //                string text = string.Concat(e.Message, st);

                logger.Log(e.ToString(), LogLevels.ApplicationError);

                if (httpContext.ApplicationInstance.Mode == ApplicationMode.Prod)
                {
                    throw;
                }
                
                result = new ResponseResult(httpContext.Response, new SimpleContent
                                            {
                                                BodyContent = e.ToString(),
                                                ContentType = "text/plain"
                                            });
            }

            httpContext.Pack();
            CurrentHttpContextProvider.ThreadSpecificHttpContext = null;
            return result;
        }
    }
}
