// <copyright company="Base2art">
// Copyright (c) 2014 All Rights Reserved
// </copyright>
// <author>Scott Youngblut</author>

namespace Base2art.MonkeyTail.Api
{
    using System;
    using Base2art.Soufflot.Mvc;
    
    public class BoxModelPositionedRenderingController : GuidedPositionedRenderingController<BoxModelGuidePost>
    {
        public BoxModelPositionedRenderingController()
            : this(BoxModelGuidePost.Center, new NullRenderingController(), 0)
        {
        }

        public BoxModelPositionedRenderingController(
            BoxModelGuidePost guidepost, 
            IRenderingController renderingController, 
            int containerPriority)
            :base(guidepost ?? BoxModelGuidePost.Center, renderingController, containerPriority)
        {
        }
    }
}
