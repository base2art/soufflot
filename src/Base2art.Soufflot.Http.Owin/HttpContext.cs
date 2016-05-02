namespace Base2art.Soufflot.Http.Owin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.Collections;
    using Base2art.Soufflot.Net;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Diagnostics;
    using Base2art.Validation;

    using Microsoft.Owin;

    public class HttpContext : IHttpContext
    {
        private readonly IOwinContext context;

        private readonly HttpContextSettings settings;

        private readonly IApplication application;

        private readonly ILogger logger;

        private readonly IHttpUserLookup userLookup;

        private readonly HttpSession httpSession;

        private readonly HttpFlash httpFlash;

        private readonly HttpRequest httpRequest;

        private readonly HttpResponse httpResponse;

        private readonly ILazy<SecureCookieJarBase<KeyValuePair<string, string>>> cookieJar;
        
        public HttpContext(
            IApplication application,
            ILogger logger,
            IHttpUserLookup userLookup,
            IOwinContext context,
            HttpContextSettings settings)
        {
            application.Validate().IsNotNull();
            logger.Validate().IsNotNull();
            context.Validate().IsNotNull();
            settings.Validate().IsNotNull();
            this.application = application;
            this.logger = logger;
            this.userLookup = userLookup;
            this.context = context;
            this.settings = settings;

            this.httpSession = new HttpSession(this.context);
            this.httpFlash = new HttpFlash(this.context);
            this.httpRequest = new HttpRequest(this.userLookup, this.context.Request, this.settings);
            this.httpResponse = new HttpResponse(this.context.Response, this.settings);
            this.cookieJar = new RetryLazy<SecureCookieJarBase<KeyValuePair<string, string>>>(() => new OwinCookieJar(this.context, this.CookieJarSettings()));
        }

        public IApplication ApplicationInstance
        {
            get
            {
                return this.application;
            }
        }

        public ILogger Logger
        {
            get
            {
                return this.logger;
            }
        }

        IHttpFlash IHttpContext.Flash
        {
            get
            {
                return this.Flash;
            }
        }

        IHttpSession IHttpContext.Session
        {
            get
            {
                return this.Session;
            }
        }

        IHttpRequest IHttpContext.Request
        {
            get
            {
                return this.Request;
            }
        }

        IHttpResponse IHttpContext.Response
        {
            get
            {
                return this.Response;
            }
        }

        public HttpFlash Flash
        {
            get
            {
                return this.httpFlash;
            }
        }

        public HttpSession Session
        {
            get
            {
                return this.httpSession;
            }
        }

        public HttpRequest Request
        {
            get
            {
                return this.httpRequest;
            }
        }

        public HttpResponse Response
        {
            get
            {
                return this.httpResponse;
            }
        }

        public void Unpack()
        {
            this.GetSecureCookie(this.Session, this.settings.SessionCookieName);
            this.GetSecureCookie(this.Flash, this.settings.FlashCookieName);

            var user = this.context.Request.User;
            
            if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
            {
                this.Request.SetUser(user);
            }
            
            //            var mapTempUserColl = new Map<string, string>();
            //            //this.GetSecureCookie(mapTempUserColl, this.settings.UserCookieName);
            //            if(mapTempUserColl.Contains("name"))
            //            {
            //                this.Request.SetUserName(this.context.Request.User.Identity.Name);
            //            }
        }

        public void Pack()
        {
            bool isRedirect = 300 <= context.Response.StatusCode && context.Response.StatusCode < 400;
            if (isRedirect)
            {
                this.SetSecureCookie(this.settings.FlashCookieName, this.Flash);
            }
            else
            {
                this.context.Response.Cookies.Delete(this.settings.FlashCookieName);
            }

            this.SetSecureCookie(this.settings.SessionCookieName, this.Session);

            //            if (this.Request.User.IsAuthenticated)
            //            {
            //                var mapTempUserColl = new Map<string, string>();
            //                mapTempUserColl.Add("name", this.Request.User.UserName);
            //                this.SetSecureCookie(this.settings.UserCookieName, mapTempUserColl);
            //            }
            //            else
            //            {
            //                this.context.Response.Cookies.Delete(this.settings.UserCookieName);
            //            }
        }

        private void GetSecureCookie(IMap<string, string> coll, string cookieName)
        {
            var cookies = this.cookieJar.Value.GetSecureCookieValues(cookieName);
            foreach (var cookie in cookies)
            {
                foreach (var cookieValue in cookie)
                {
                    coll[cookie.Key] = cookieValue;
                }
            }
        }

        private void SetSecureCookie(string cookieName, IMap<string, string> values)
        {
            var mm = new MultiMap<string, string>();
            foreach (var pair in values)
            {
                mm.Add(pair.Key, pair.Value);
            }
            
            this.cookieJar.Value.SetSecureCookieValues(cookieName, mm);
        }

        private SecureCookieJarSettings CookieJarSettings()
        {
            return new SecureCookieJarSettings
            {
                ApplicationSaltSettings = settings.ApplicationSaltSettings,
                SecureCookiePrefix = settings.SecureCookiePrefix
            };
        }
    }
}
