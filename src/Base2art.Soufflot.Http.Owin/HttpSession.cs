namespace Base2art.Soufflot.Http.Owin
{
    using Base2art.Collections;

    using Microsoft.Owin;

    public class HttpSession : Map<string, string>, IHttpSession
    {
        private readonly IOwinContext context;

        public HttpSession(IOwinContext context)
        {
            this.context = context;
        }
    }
}
