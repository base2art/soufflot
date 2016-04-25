namespace Base2art.Soufflot.Net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class KeyValuePairCookieJar : SecureCookieJarBase<KeyValuePair<string, string>>
    {
        private readonly IDictionary<string, string> context;

        public KeyValuePairCookieJar(SecureCookieJarSettings settings)
            : this(new Dictionary<string, string>(), settings)
        {
        }

        public KeyValuePairCookieJar(IDictionary<string, string> context, SecureCookieJarSettings settings) : base(settings)
        {
            this.context = context;
        }

        protected override KeyValuePair<string, string> GetCookieByName(string cookieName)
        {
            if (this.context.ContainsKey(cookieName))
            {
                return new KeyValuePair<string, string>(cookieName, this.context[cookieName]);
            }
            
            return default(KeyValuePair<string, string>);
        }

        protected override string GetValueFromCookie(KeyValuePair<string, string> cookie)
        {
            return cookie.Value;
        }

        protected override void SetCookie(string name, string value)
        {
            this.context[name] = value;
        }

        protected override void DeleteCookie(string cookieName)
        {
            if (this.context.ContainsKey(cookieName))
            {
                this.context.Remove(cookieName);
            }
        }
    }
}
