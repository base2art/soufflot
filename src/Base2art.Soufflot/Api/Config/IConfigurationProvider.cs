namespace Base2art.Soufflot.Api.Config
{
    public interface IConfigurationProvider
    {
        string GetValue(string key);
    }
}