namespace Base2art.Soufflot.Api
{
    using Base2art.Soufflot.Mvc;

    public class SimpleResult : IResult
    {
        public IContent Content { get; set; }

        public IResult As(string contentType)
        {
            var cntnt = this.Content as SimpleContent;

            if (cntnt != null)
            {
                cntnt.ContentType = contentType;
            }

            return this;
        }
    }
}