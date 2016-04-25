namespace Base2art.Soufflot.Http
{
    public interface ICurrentHttpContextProvider
    {
        IHttpContext Current { get; }
    }
}
