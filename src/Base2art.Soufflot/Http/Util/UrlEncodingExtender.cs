namespace Base2art.Soufflot.Http.Util
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.Collections;

    public static class UrlEncodingExtender
    {
        public static IReadOnlyMultiMap<string, string> ParseValue(string value)
        {
            var multiMap = new MultiMap<string, string>();
            ParseValue(multiMap, value);
            return multiMap;
        }

        public static void ParseValue(IMultiMap<string, string> result, string value)
        {
            var clean = value.TrimStart('?');
            var parts = clean.Split('&');

            foreach (var part in parts)
            {
                var subParts = part.Split(new[] { '=' }, 2, StringSplitOptions.None);

                var key = Uri.UnescapeDataString(subParts[0]);

                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                result.Add(key, subParts.Length == 1 ? string.Empty : Uri.UnescapeDataString(subParts[1]));
            }
        }

        public static void ParseValue(IMap<string, string> result, string value)
        {
            var clean = value.TrimStart('?');
            var parts = clean.Split('&');

            foreach (var part in parts)
            {
                var subParts = part.Split(new[] { '=' }, 2, StringSplitOptions.None);

                var key = Uri.UnescapeDataString(subParts[0]);

                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                result[key] = subParts.Length == 1 ? string.Empty : Uri.UnescapeDataString(subParts[1]);
            }
        }

        public static string Write(IReadOnlyMap<string, string> flash)
        {
            List<string> sb = new List<string>();
            foreach (var key in flash.Keys)
            {
                sb.Add(string.Concat(Uri.EscapeDataString(key), "=", Uri.EscapeDataString(flash[key] ?? string.Empty)));
            }

            return string.Join("&", sb);
        }
    }
}
