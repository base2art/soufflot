// <copyright company="Base2art">
// Copyright (c) 2014 All Rights Reserved
// </copyright>
// <author>Scott Youngblut</author>

namespace Base2art.MonkeyTail.Api
{
    using System;
    using Base2art.Soufflot.Api;
    
    public class BoxModelPositionedRenderingController : GuidedPositionedRenderingController<BoxModelGuidePost>
    {
        public BoxModelPositionedRenderingController()
            : this(BoxModelGuidePost.Center, new NullRenderingRouted(), 0)
        {
        }

        public BoxModelPositionedRenderingController(
            BoxModelGuidePost guidepost, 
            IRenderingRouted renderingController, 
            int containerPriority)
            :base(guidepost ?? BoxModelGuidePost.Center, renderingController, containerPriority)
        {
        }
    }
}
