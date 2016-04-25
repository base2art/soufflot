namespace Base2art.Soufflot.Mvc
{
    public interface IResult
    {
        IResult As(string contentType);

        IContent Content { get; }
    }
}