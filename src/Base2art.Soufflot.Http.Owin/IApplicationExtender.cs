
namespace Base2art.Soufflot.Http.Owin
{
    public interface IApplicationExtender
    {
        global::Owin.IAppBuilder Configure(global::Owin.IAppBuilder app);
    }
}
