namespace Base2art.Soufflot.Api
{
    public interface IResult
    {
        IResult As(string contentType);

        IContent Content { get; }
    }
}