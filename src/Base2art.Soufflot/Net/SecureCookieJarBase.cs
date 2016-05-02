namespace Base2art.Soufflot.Net
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Base2art.Collections;
    using Base2art.Validation;
    
    public abstract class SecureCookieJarBase<TCookie>
    {
        private readonly SecureCookieJarSettings settings;
        
        protected SecureCookieJarBase(SecureCookieJarSettings settings)
        {
            this.settings = settings;
        }

        public void SetSecureCookieValues(string cookieName, IReadOnlyMultiMap<string, string> secureCollection)
        {
            secureCollection.Validate().IsNotNull();
            
            if (secureCollection.Count == 0)
            {
                this.DeleteCookie(cookieName);
            }
            else
            {
                var serializedCollection = UrlEncodingExtender.Write(secureCollection);
                var checkSum = this.GenerateCompositeHash(cookieName, serializedCollection);
                this.SetCookie(cookieName, string.Concat(this.Prefix(), checkSum, "?", serializedCollection));
            }
        }
        
        public IReadOnlyMultiMap<string, string> GetSecureCookieValues(string cookieName)
        {
            IMultiMap<string, string> map = new MultiMap<string, string>();
            this.PopulateCollectionFromSecureCookie(cookieName, map);
            return map;
        }
        
        public string GetCookieValue(string cookieName)
        {
            var cookie = this.GetCookieByName(cookieName);
            return cookie == null
                ? string.Empty
                : this.GetValueFromCookie(cookie);
        }

        public void SetCookieValue(string cookieName, string cookieValue)
        {
            this.SetCookie(cookieName, cookieValue);
        }
        
        public void DeleteSecureCookie(string cookieName)
        {
            this.DeleteCookie(cookieName);
        }

        protected abstract TCookie GetCookieByName(string cookieName);
        
        protected abstract string GetValueFromCookie(TCookie cookie);
        
        protected abstract void SetCookie(string name, string value);
        
        protected abstract void DeleteCookie(string cookieName);
        
        private string GenerateCompositeHash(string cookieName, string serializedCollection)
        {
            return
                HashingExtensions.Hash(serializedCollection + "#" + this.settings.ApplicationSaltSettings + ":" + cookieName)
                .AsString();
        }
        
        private void PopulateCollectionFromSecureCookie(string cookieName, IMultiMap<string, string> map)
        {
            var item = this.GetCookieByName(cookieName);
            var cookieValue = this.GetValueFromCookie(item);

            if (string.IsNullOrWhiteSpace(cookieValue))
            {
                return;
            }

            var builder = new UriBuilder("http://domain.com" + cookieValue);
            string serializedCollection = builder.Query.TrimStart('?');
            var path = builder.Path;
            
            string checkSum = path.StartsWith(this.Prefix(), StringComparison.Ordinal)
                ? path.Substring(this.Prefix().Length)
                : string.Empty;

            if (checkSum != this.GenerateCompositeHash(cookieName, serializedCollection))
            {
                return;
            }

            UrlEncodingExtender.ParseValue(map, serializedCollection);
        }

        private string Prefix()
        {
            var secureCookiePrefix = this.settings.SecureCookiePrefix;
            if (string.IsNullOrWhiteSpace(secureCookiePrefix))
            {
                secureCookiePrefix = "scs";
            }
            
            return string.Format(CultureInfo.InvariantCulture, "/{0}/", secureCookiePrefix);
        }
        
        private interface IHashResult
        {
            string AsString();

            Guid AsGuid();
        }
        
        private static class HashingExtensions
        {
            public static IHashResult Hash(string value)
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(value);
                using (MD5 md5 = MD5.Create())
                {
                    return new Md5HashResult(md5.ComputeHash(inputBytes));
                }
            }

            private class Md5HashResult : IHashResult
            {
                private readonly byte[] hashedBytes;

                public Md5HashResult(byte[] hashedBytes)
                {
                    this.hashedBytes = hashedBytes;
                }

                public string AsString()
                {
                    var hash = this.hashedBytes;
                    
                    // step 2, convert byte array to hex string
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        sb.Append(hash[i].ToString("X2", CultureInfo.InvariantCulture));
                    }
                    
                    return sb.ToString();
                }

                public Guid AsGuid()
                {
                    var bytes = new byte[16];
                    bytes[0] = this.hashedBytes[3];
                    bytes[1] = this.hashedBytes[2];
                    bytes[2] = this.hashedBytes[1];
                    bytes[3] = this.hashedBytes[0];
                    bytes[4] = this.hashedBytes[5];
                    bytes[5] = this.hashedBytes[4];
                    bytes[6] = this.hashedBytes[7];
                    bytes[7] = this.hashedBytes[6];
                    bytes[8] = this.hashedBytes[8];
                    bytes[9] = this.hashedBytes[9];
                    bytes[10] = this.hashedBytes[10];
                    bytes[11] = this.hashedBytes[11];
                    bytes[12] = this.hashedBytes[12];
                    bytes[13] = this.hashedBytes[13];
                    bytes[14] = this.hashedBytes[14];
                    bytes[15] = this.hashedBytes[15];
                    return new Guid(bytes);
                }
            }
        }
    }
}
