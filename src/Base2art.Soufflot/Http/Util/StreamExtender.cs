namespace Base2art.Soufflot.Http.Util
{
    using System.IO;
    using System.Text;

    public static class StreamExtender
    {
        public static ByteArrayReadResult ReadFully(this Stream stream)
        {
            return ReadFully(stream, 0);
        }

        public static ByteArrayReadResult ReadFully(this Stream stream, int maxByteSize)
        {
            // Jon Skeet's accepted answer 
            byte[] buffer = new byte[16 * 1024];
            var currentlyRead = 0;
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                    currentlyRead += read;
                    if (maxByteSize != 0 && currentlyRead > maxByteSize)
                    {
                        return new ByteArrayReadResult(ms.ToArray(), true);
                    }
                }

                return new ByteArrayReadResult(ms.ToArray(), false);
            }
        }

        public static StringReadResult ReadFullyAsString(this Stream stream)
        {
            return ReadFullyAsString(stream, 0);
        }

        public static StringReadResult ReadFullyAsString(this Stream stream, int maxByteSize)
        {
            char[] buffer = new char[16 * 1024];
            var currentlyRead = 0;
            StringBuilder sb = new StringBuilder();

            using (var sr = new StreamReader(stream))
            {
                int read;
                while ((read = sr.Read(buffer, 0, buffer.Length)) > 0)
                {
                    sb.Append(buffer, 0, read);
                    currentlyRead += read;
                    if (maxByteSize != 0 && currentlyRead > maxByteSize)
                    {
                        return new StringReadResult(sb.ToString(), true);
                    }
                }

                return new StringReadResult(sb.ToString(), false);
            }
        }
    }
}

//            var memoryStream = stream as MemoryStream;
//            if (memoryStream != null)
//            {
//                return memoryStream.ToArray();
//            }
