namespace Base2art.Soufflot.Api.Config
{
    using System.IO;

    using Base2art.Collections;

    public class Properties : Map<string, string>
    {
        public static Properties Load(Stream stream)
        {
            var properties = new Properties();

            var streamReader = new StreamReader(stream);

            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
                AddTo(properties, str);
            }

            return properties;
        }

        private static void AddTo(Properties properties, string line)
        {
            bool isValidLine = (!string.IsNullOrEmpty(line));
            if (isValidLine)
            {
                isValidLine &= (!line.StartsWith(";"));
                isValidLine &= (!line.StartsWith("#"));
                isValidLine &= (!line.StartsWith("'"));
                isValidLine &= (line.Contains("="));
            }

            if (isValidLine)
            {
                int index = line.IndexOf('=');
                string key = line.Substring(0, index).Trim();
                string value = line.Substring(index + 1).Trim();

                if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                    (value.StartsWith("'") && value.EndsWith("'")))
                {
                    value = value.Substring(1, value.Length - 2);
                }

                properties.Add(key, value);
            }
        }
    }
}
