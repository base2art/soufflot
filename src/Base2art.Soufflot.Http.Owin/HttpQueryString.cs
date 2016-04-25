namespace Base2art.Soufflot.Http.Owin
{
    using Base2art.Collections;
    using Base2art.Soufflot.Http.Util;

    public class HttpQueryString : MultiMap<string, string>, IHttpQueryString
    {
        private readonly string originalValue;

        public HttpQueryString(string originalValue)
        {
            this.originalValue = (originalValue ?? string.Empty).Trim('?');
            UrlEncodingExtender.ParseValue(this, this.originalValue);
        }

        public string OriginalValue
        {
            get
            {
                return this.originalValue;
            }
        }
    }
}