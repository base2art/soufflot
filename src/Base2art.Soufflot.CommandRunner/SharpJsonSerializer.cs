namespace Base2art.Soufflot.CommandRunner
{
    using Base2art.MonkeyTail;
    using Base2art.Serialization;

    public class SharpJsonSerializer : IJsonDeserializer
    {
        private readonly ISerializer serializer;

        public SharpJsonSerializer()
        {
            this.serializer = new NewtonsoftSerializer();
        }

        public T Deserialize<T>(string stream)
        {
            return this.serializer.Deserialize<T>(stream);
        }

        public string Serialize<T>(T value)
        {
            return this.serializer.Serialize<T>(value);
        }
    }
}