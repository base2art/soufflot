namespace Base2art.Soufflot.Api.Fixtures
{
    using Base2art.Soufflot.Mvc;

    using Base2art.Soufflot.Http;

    public class SubCountingNonRenderingController : INonRenderingRouted
    {
        public static int Count = 0;

        public INonRenderingRouted[] NonRenderingControllers
        {
            get { return new INonRenderingRouted[0]; }
        }

        public void Execute(IHttpContext context)
        {
            Count++;
        }
    }
}
