namespace Base2art.Soufflot.Http.Util
{
    public class ByteArrayReadResult : ReadResultBase
    {
        private readonly byte[] value;

        public ByteArrayReadResult(byte[] value, bool maxLengthExceded)
            :base(maxLengthExceded)
        {
            this.value = value;
        }

        public byte[] Value
        {
            get
            {
                return this.value;
            }
        }
    }
}
