namespace Base2art.Soufflot.Http.Owin.Fixtures
{
    using System;

    using global::Owin;

    public class DomainWrapper : MarshalByRefObject, IDomainWrapper
    {
        public string Root
        {
            get
            {
                IAppBuilder builder = null;
                return builder.BaseDirectory();
            }
        }
    }
}