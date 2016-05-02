namespace Base2art.Soufflot.Http
{
    using Base2art.Collections;

    public interface IHttpUser
    {
        bool IsAuthenticated { get; }

        string UserName { get; }

        IReadOnlyArrayList<string> GroupNames { get; }
    }
}
