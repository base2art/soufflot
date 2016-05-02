using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base2art.Soufflot.Api.Fixtures
{
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class ControllerThrowsException:IRenderingRouted
    {
        public IPositionedRenderingRouted[] RenderingControllers
        {
            get
            {
                return new IPositionedRenderingRouted[0];
            }
        }

        public INonRenderingRouted[] NonRenderingControllers
        {
            get
            {
                return new INonRenderingRouted[0];
            }
        }

        public IResult Execute(IHttpContext httpContext, List<PositionedResult> childResults)
        {
            throw new NotImplementedException();
        }
    }
}
