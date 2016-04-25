namespace Base2art.Soufflot.Http.Owin
{
    using Base2art.Collections;

    using Microsoft.Owin;

    public class HttpFlash : Map<string, string>, IHttpFlash
    {
        private readonly IOwinContext context;

        public HttpFlash(IOwinContext context)
        {
            this.context = context;
        }
    }
}