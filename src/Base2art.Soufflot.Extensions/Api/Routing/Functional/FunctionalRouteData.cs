namespace Base2art.Soufflot.Api.Routing.Functional
{
    public class FunctionalRouteData<T> : RouteData<T>
    {
        public FunctionalRouteData(IClass<T> controllerClass)
            : base(controllerClass, null)
        {
        }
    }
}
