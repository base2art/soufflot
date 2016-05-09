namespace Base2art.Soufflot.RouteCompiler
{
    public class RouteCompiler
    {
        private readonly string routeFile;

        private readonly string appNameRoutes;

        private readonly string location;

        public RouteCompiler(string routeFile, string reverseRouteDllName, string routeBasePath)
        {
            this.location = routeBasePath;
            this.appNameRoutes = reverseRouteDllName;
            this.routeFile = routeFile;
        }
        
    }
}