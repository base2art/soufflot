
namespace Base2art.Soufflot.Http
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text.RegularExpressions;

    using System.Xml.Linq;
    using Base2art.Collections;
    using Base2art.Soufflot.Http.Util;
    using Base2art.Serialization;

    public static class HttpRequestBody
    {
        private static readonly NewtonsoftSerializer serializer = new Base2art.Serialization.NewtonsoftSerializer();
        
        private static readonly Regex regex = new Regex(@"^(.*)\[(\d+)\]$");
        
        public static T As<T>(this IHttpRequest request)
        {
            IHttpReadOnlyHeaderCollection headers = request.Headers;
            if (headers.Contains("content-type"))
            {
                var contentType = headers.GetFirstOrNull("content-type");
                
                if (string.Equals("application/x-www-form-urlencoded", contentType, System.StringComparison.OrdinalIgnoreCase))
                {
                    return HydrateFromFormUrlEncoded<T>(request.RequestBody.AsFormUrlEncoded());
                }
                
                if (string.Equals("application/json", contentType, System.StringComparison.OrdinalIgnoreCase))
                {
                    return serializer.Deserialize<T>(request.RequestBody.AsText());
                }
            }
            
            return request.RequestBody.AsBinary<T>();
        }
        
        public static T AsBinary<T>(this IHttpRequestBody body)
        {
            var bytes = body.AsRaw();
            if (body.IsMaxSizeExceeded)
            {
                return default(T);
            }

            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0L, SeekOrigin.Begin);
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(ms);
            }
        }

        public static IReadOnlyMultiMap<string, string> AsFormUrlEncoded(this IHttpRequestBody body)
        {
            var bytes = body.AsText();
            if (body.IsMaxSizeExceeded)
            {
                return new MultiMap<string, string>();
            }

            return UrlEncodingExtender.ParseValue(bytes);
        }

        public static XElement AsXml(this IHttpRequestBody body)
        {
            var bytes = body.AsRaw();
            if (body.IsMaxSizeExceeded)
            {
                return null;
            }

            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0L, SeekOrigin.Begin);
                return XElement.Load(ms);
            }
        }

        public static dynamic AsJson(this IHttpRequestBody body)
        {
            return serializer.Deserialize<dynamic>(body.AsText());
        }
        
        private static T HydrateFromFormUrlEncoded<T>(IReadOnlyMultiMap<string, string> data)
        {
            var obj = new ExpandoObject();
            
            foreach (var item in data)
            {
                Fill(obj, item.Key, item);
            }
            
            var serVal = serializer.Serialize(obj);
//            Console.WriteLine(serVal);
            return serializer.Deserialize<T>(serVal);
        }


        private static void Fill(IDictionary<string, object> obj, string key, IEnumerable<string> values)
        {
            var items = key.Split(new []{'.'}, System.StringSplitOptions.RemoveEmptyEntries);
            
            FillPath(obj, items, 0, values);
        }

        private static void FillPath(IDictionary<string, object> obj, string[] items, int i, IEnumerable<string> values)
        {
            if (items.Length == i + 1)
            {
                obj[items[i]] = values.FirstOrDefault();
                return;
            }
            
            var key = items[i];
            
            var match = regex.Match(key);
            
            if (match != null && match.Success)
            {
                var cleanKey = match.Groups[1].Value;
                var index = int.Parse(match.Groups[2].Value);
                if (!obj.ContainsKey(cleanKey))
                {
                    obj[cleanKey] = new List<ExpandoObject>();
                }
                
                var arr = obj[cleanKey] as List<ExpandoObject> ?? new List<ExpandoObject>();
                obj[cleanKey] = arr;
                
                for (int j = arr.Count; j <= index; j++)
                {
                    arr.Add(new ExpandoObject());
                }
                
                FillPath(arr[index], items, i + 1, values);
            }
            else if (obj.ContainsKey(items[i]) && obj[items[i]] is List<ExpandoObject>)
            {
            }
            else
            {
                var newObj = new ExpandoObject();
                FillPath(newObj, items, i + 1, values);
                obj[items[i]] = newObj;
            }
        }
    }
}

