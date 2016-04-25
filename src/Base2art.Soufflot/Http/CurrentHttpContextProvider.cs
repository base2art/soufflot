namespace Base2art.Soufflot.Http
{
    using System;

    public class CurrentHttpContextProvider : ICurrentHttpContextProvider
    {
        [ThreadStatic]
        public static IHttpContext ThreadSpecificHttpContext;

        public IHttpContext Current
        {
            get
            {
                return ThreadSpecificHttpContext;
            }
        }
    }
}
