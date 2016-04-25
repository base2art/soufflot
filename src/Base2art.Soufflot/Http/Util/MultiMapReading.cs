namespace Base2art.Soufflot.Http.Util
{
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.Collections;

    public static class MultiMapReading
    {
        public static IEnumerable<TValue> GetOrNull<TKey, TValue>(this IReadOnlyMultiMap<TKey, TValue> coll, TKey key)
        {
            if (!coll.Contains(key))
            {
                return null;
            }

            return coll[key];
        }

        public static IEnumerable<TValue> GetOrEmpty<TKey, TValue>(this IReadOnlyMultiMap<TKey, TValue> coll, TKey key)
        {
            if (!coll.Contains(key))
            {
                return new TValue[0];
            }

            return coll[key];
        }

        public static TValue GetFirstOrNull<TKey, TValue>(this IReadOnlyMultiMap<TKey, TValue> coll, TKey key)
        {
            if (!coll.Contains(key))
            {
                return default(TValue);
            }

            return coll[key].FirstOrDefault();
        }

        public static string GetFirstOrEmpty(this IReadOnlyMultiMap<string, string> coll, string key)
        {
            if (!coll.Contains(key))
            {
                return string.Empty;
            }

            return coll[key].FirstOrDefault() ?? string.Empty;
        }
    }
}