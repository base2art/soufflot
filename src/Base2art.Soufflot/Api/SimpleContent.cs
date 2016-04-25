namespace Base2art.Soufflot.Api
{
    using Base2art.Soufflot.Mvc;

    public class SimpleContent : IContent
    {
        
        public byte[] Body { get; set; }
        
        public string BodyContent
        {
            get
            {
                return System.Text.Encoding.Default.GetString(this.Body);
            }
            
            set
            {
                this.Body = System.Text.Encoding.Default.GetBytes(value);
            }
        }

        
        public string BodyAsString
        {
            get
            {
                return this.BodyContent;
            }
        }

        public string ContentType { get; set; }
    }
}