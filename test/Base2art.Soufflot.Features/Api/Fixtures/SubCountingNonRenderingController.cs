namespace Base2art.Soufflot.Api.Fixtures
{
    using Base2art.Soufflot.Mvc;

    using Base2art.Soufflot.Http;

    public class SubCountingNonRenderingController : INonRenderingController
    {
        public static int Count = 0;
        public INonRenderingController[] NonRenderingControllers
        {
            get
            {
                return new INonRenderingController[0];
            }
        }

        public void Execute(IHttpContext context)
        {
            Count++;
        }
    }
}