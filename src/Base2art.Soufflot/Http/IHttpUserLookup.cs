namespace Base2art.Soufflot.Http
{
    using System.Security.Principal;

	public interface IHttpUserLookup
	{
		IHttpUser FindUser(IPrincipal principal);
	}
}

