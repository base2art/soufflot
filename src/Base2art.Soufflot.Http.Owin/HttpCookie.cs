namespace Base2art.Soufflot.Http.Owin
{
    public class HttpCookie : IHttpCookie
    {
        private readonly string name;

        private readonly string value;

        public HttpCookie(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
        }
    }
}