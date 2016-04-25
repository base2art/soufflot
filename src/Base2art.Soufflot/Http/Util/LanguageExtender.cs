namespace Base2art.Soufflot.Http.Util
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public static class LanguageExtender
    {
        public static CultureInfo[] ToCultures(string langsString)
        {
            var items = new List<CultureInfo>();
            var langs = langsString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(TryConvert)
                .Where(x => x != null);
            items.AddRange(langs);
            return items.ToArray();
        }

        private static CultureInfo TryConvert(string arg)
        {
            try
            {
                return CultureInfo.CreateSpecificCulture(arg);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
