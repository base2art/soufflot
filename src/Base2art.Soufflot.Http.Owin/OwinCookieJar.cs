namespace Base2art.Soufflot.Http.Owin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base2art.Soufflot.Net;
    using Microsoft.Owin;
    
    public class OwinCookieJar : SecureCookieJarBase<KeyValuePair<string, string>>
    {
        private readonly IOwinContext context;

        public OwinCookieJar(IOwinContext context, SecureCookieJarSettings settings) : base(settings)
        {
            this.context = context;
        }

        #region implemented abstract members of SecureCookieJarBase

        protected override KeyValuePair<string, string> GetCookieByName(string cookieName)
        {
            return this.context.Request.Cookies.FirstOrDefault(x => x.Key == cookieName);
        }

        protected override string GetValueFromCookie(KeyValuePair<string, string> cookie)
        {
            return cookie.Value;
        }

        protected override void SetCookie(string name, string value)
        {
            this.context.Response.Cookies.Append(name, value);
        }

        protected override void DeleteCookie(string cookieName)
        {
            this.context.Response.Cookies.Delete(cookieName);
        }

        #endregion
    }
}


