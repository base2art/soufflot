namespace Base2art.Soufflot.Linq
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ArrayExtensions
    {
        public static IEnumerable<T> Coalesce<T>(this T[] items)
            where T : class
        {
            if (items == null)
            {
                return new T[0];
            }

            return items.Where(x => x != null);
        }

        public static T[] CoalesceValues<T>(this T[] items)
            where T : struct
        {
            if (items == null)
            {
                return new T[0];
            }

            return items;
        }
    }
}
