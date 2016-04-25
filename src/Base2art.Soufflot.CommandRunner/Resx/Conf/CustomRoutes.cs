namespace App.Conf
{
    using Base2art.Soufflot.Api.Routing.Expressive;
    using Base2art.Soufflot.Http;

    public class CustomRoutes : MappedLocalHostExpressiveRouter
    {
        // Change this if you need to support multiple Domains
        // This should be the primary domain that you currently testing
        public CustomRoutes()
            : base("localhost", new CurrentHttpContextProvider())
        {
            // REGISTER ROUTES HERE
            /*
            this.RegisterRoute<HomeController>(HttpMethod.Get, null, "/");
            this.RegisterRoute<HomeController>(HttpMethod.Get, null, "/about-us", (a, b, c) => a.AboutUs(b, c));
             */ 
        }
    }
}
