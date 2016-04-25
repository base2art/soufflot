using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base2art.Soufflot.Api.Fixtures
{
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class ControllerThrowsException:IRenderingController
    {
        public IPositionedRenderingController[] RenderingControllers
        {
            get
            {
                return new IPositionedRenderingController[0];
            }
        }

        public INonRenderingController[] NonRenderingControllers
        {
            get
            {
                return new INonRenderingController[0];
            }
        }

        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            throw new NotImplementedException();
        }
    }
}
