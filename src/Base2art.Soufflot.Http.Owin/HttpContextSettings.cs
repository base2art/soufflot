namespace Base2art.Soufflot.Http.Owin
{
    using System;

    public class HttpContextSettings
    {
        private string flashCookieName;

        private string sessionCookieName;

        private string applicationSaltSettings;

        private string secureCookiePrefix;

        private int maxRequestSize;

        public string SecureCookiePrefix
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.secureCookiePrefix))
                {
                    return "scs";
                }

                return this.secureCookiePrefix;
            }
            set
            {
                this.secureCookiePrefix = value;
            }
        }

        public string FlashCookieName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.flashCookieName))
                {
                    return "flash";
                }

                return this.flashCookieName;
            }
            set
            {
                this.flashCookieName = value;
            }
        }

        public string SessionCookieName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.sessionCookieName))
                {
                    return "session";
                }

                return this.sessionCookieName;
            }
            set
            {
                this.sessionCookieName = value;
            }
        }

        public string ApplicationSaltSettings
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.applicationSaltSettings))
                {
                    throw new InvalidOperationException("YOU MUST set the appSalt or you have security vulnerabilities");
                }

                return this.applicationSaltSettings;
            }
            set
            {
                this.applicationSaltSettings = value;
            }
        }

        public int MaxRequestSizeBytes
        {
            get
            {
                // 4 MB
                return this.maxRequestSize == 0 ? (1024 * 1024 * 4) : this.maxRequestSize;
            }
            set
            {
                this.maxRequestSize = value;
            }
        }
    }
}
