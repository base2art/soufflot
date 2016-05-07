namespace Base2art.Soufflot.Http.Util
{
    public class StringReadResult : ReadResultBase
    {
        private readonly string value;

        public StringReadResult(string value, bool maxLengthExceded)
            : base(maxLengthExceded)
        {
            this.value = value;
        }

        public string Value
        {
            get { return this.value; }
        }
    }
}
