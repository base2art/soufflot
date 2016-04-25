namespace Base2art.Soufflot.CommandRunner
{
    using System;

    public static class ExceptionUtil
    {
        public static T Retry<T>(this Func<T> func, int tries, int counter = 0)
        {
            try
            {
                return func();
            }
            catch (Exception)
            {
                if (counter >= tries)
                {
                    throw;
                }

                return Retry<T>(func, tries, counter +1);
            }
        }
    }
}