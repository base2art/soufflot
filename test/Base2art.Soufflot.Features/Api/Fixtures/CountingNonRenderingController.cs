namespace Base2art.Soufflot.Api.Fixtures
{
    using Base2art.Soufflot.Mvc;

    using Base2art.Soufflot.Http;

    public class CountingNonRenderingController : INonRenderingController
    {
        public static int Count = 0;
        public INonRenderingController[] NonRenderingControllers
        {
            get
            {
                return new INonRenderingController[] { new SubCountingNonRenderingController() };
            }
        }

        public void Execute(IHttpContext context)
        {
            Count++;
        }
    }
}