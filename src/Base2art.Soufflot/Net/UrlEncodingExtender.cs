namespace Base2art.Soufflot.Net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base2art.Collections;
    using Base2art.Validation;

    public static class UrlEncodingExtender
    {
        public static IReadOnlyMultiMap<string, string> ParseValuesAllowingDuplicates(string value)
        {
            var multiMap = new MultiMap<string, string>();
            ParseValue(multiMap, value);
            return multiMap;
        }

        public static void ParseValue(IMultiMap<string, string> collection, string value)
        {
            ParseValueInternal(
                value,
                collection.Add);
        }
        
        public static IReadOnlyMap<string, string> ParseValuesNoDuplicates(string value)
        {
            var multiMap = new Map<string, string>();
            ParseValue(multiMap, value);
            return multiMap;
        }
        
        public static void ParseValue(IMap<string, string> collection, string value)
        {
            ParseValueInternal(
                value,
                (y, z) => collection[y] = z);
        }

        public static void ParseValueInternal(string value, Action<string, string> updateFunction)
        {
            value.Validate().IsNotNull();
            updateFunction.Validate().IsNotNull();
            
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
                
                updateFunction(key, subParts.Length == 1 ? string.Empty : Uri.UnescapeDataString(subParts[1]));
            }
        }
        
        public static string Write(IReadOnlyMap<string, string> collection)
        {
            return WriteInternal(collection, key => new[] { collection[key] ?? string.Empty });
        }
        
        public static string Write(IReadOnlyMultiMap<string, string> collection)
        {
            return WriteInternal(collection, key => (collection[key] ?? new string[0]).ToArray());
        }
        
        public static string WriteInternal(IReadOnlyMapping<string, string> collection, Func<string, string[]> getValuesForKey)
        {
            collection.Validate().IsNotNull();
            
            List<string> sb = new List<string>();
            foreach (var key in collection.Keys)
            {
                var valuesForKey = getValuesForKey(key);
                
                foreach (var valueForKey in valuesForKey)
                {
                    sb.Add(string.Concat(Uri.EscapeDataString(key), "=", Uri.EscapeDataString(valueForKey ?? string.Empty)));
                }
            }
            
            return string.Join("&", sb);
        }
    }
}
