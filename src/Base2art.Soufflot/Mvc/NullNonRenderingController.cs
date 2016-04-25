namespace Base2art.Soufflot.Mvc
{
	public class NullNonRenderingController : INonRenderingController
	{
		void INonRenderingController.Execute(Base2art.Soufflot.Http.IHttpContext httpContext)
		{
		}

		INonRenderingController[] INonRenderingController.NonRenderingControllers
		{
			get
			{
				return new INonRenderingController[0];
			}
		}
	}
}


