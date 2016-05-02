namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using Base2art.Soufflot.Mvc;

    public interface IExpressiveReverseRouter
    {
        IRoutable<TController> For<TController>()
            where TController : IRenderingRouted;

    }
}