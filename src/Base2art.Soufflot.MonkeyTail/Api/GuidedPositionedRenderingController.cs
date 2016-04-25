// <copyright company="Base2art">
// Copyright (c) 2014 All Rights Reserved
// </copyright>
// <author>Scott Youngblut</author>

namespace Base2art.MonkeyTail.Api
{
    using System;
    using Base2art.Soufflot.Mvc;
	using Base2art.Validation;
    
    public class GuidedPositionedRenderingController<TGuidepost> : IPositionedRenderingController 
        where TGuidepost : IValueContainerOut<int>, IComparable<TGuidepost>, new()
    {
        private TGuidepost guidepost;
		
        public GuidedPositionedRenderingController() : this(default(TGuidepost), new NullRenderingController(), 0)
        {
        }

        public GuidedPositionedRenderingController(
            TGuidepost guidepost, 
            IRenderingController renderingController, 
            int containerPriority)
        {
            renderingController.Validate().IsNotNull();
            this.Guidepost = guidepost;
            this.RenderingController = renderingController;
            this.ContainerPriority = containerPriority;
        }

        public TGuidepost Guidepost
        {
            get
            {
                // disable once CompareNonConstrainedGenericWithNull
                return this.guidepost == null || this.guidepost.CompareTo(default(TGuidepost)) == 0 
			        ? new TGuidepost()
                    : this.guidepost;
            }
            
            set
            {
                guidepost = value;
            }
        }

        public IRenderingController RenderingController { get; set; }

        public int Container
        {
            get { return this.Guidepost.Value; }
        }

        public int ContainerPriority { get; set; }
    }
}




