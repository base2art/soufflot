namespace Base2art.Soufflot.Http.Owin
{
    using Base2art.Soufflot.Http.Util;

    using Microsoft.Owin;

    public class HttpRequestBody : IHttpRequestBody
    {
        private readonly IOwinRequest request;

        private readonly HttpContextSettings settings;

        public HttpRequestBody(IOwinRequest request, HttpContextSettings settings)
        {
            this.request = request;
            this.settings = settings;
        }

        public bool IsMaxSizeExceeded { get; set; }

        public byte[] AsRaw()
        {
            ByteArrayReadResult byteArrayReadResult = this.request.Body.ReadFully(this.settings.MaxRequestSizeBytes);
            this.IsMaxSizeExceeded = byteArrayReadResult.MaxLengthExceded;
            return byteArrayReadResult.Value;
        }

        public string AsText()
        {
            StringReadResult byteArrayReadResult = this.request.Body.ReadFullyAsString(this.settings.MaxRequestSizeBytes);
            this.IsMaxSizeExceeded = byteArrayReadResult.MaxLengthExceded;
            return byteArrayReadResult.Value;
        }
    }
}