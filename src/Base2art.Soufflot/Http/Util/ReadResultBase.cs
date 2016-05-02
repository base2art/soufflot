namespace Base2art.Soufflot.Http.Util
{
    public class ReadResultBase
    {
        private readonly bool maxLengthExceded;

        public ReadResultBase(bool maxLengthExceded)
        {
            this.maxLengthExceded = maxLengthExceded;
        }

        public bool MaxLengthExceded
        {
            get
            {
                return this.maxLengthExceded;
            }
        }
    }
}
