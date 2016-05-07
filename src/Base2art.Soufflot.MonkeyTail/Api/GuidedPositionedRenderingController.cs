// <copyright company="Base2art">
// Copyright (c) 2014 All Rights Reserved
// </copyright>
// <author>Scott Youngblut</author>

namespace Base2art.MonkeyTail.Api
{
    using System;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Mvc;
    using Base2art.Validation;
    
    public class GuidedPositionedRenderingController<TGuidepost> : IPositionedRenderingRouted
        where TGuidepost : IValueContainerOut<int>, IComparable<TGuidepost>, new()
    {
        private TGuidepost guidepost;
        
        public GuidedPositionedRenderingController()
            : this(default(TGuidepost), new NullRenderingRouted(), 0)
        {
        }

        public GuidedPositionedRenderingController(
            TGuidepost guidepost, 
            IRenderingRouted renderingController, 
            int containerPriority)
        {
            renderingController.Validate().IsNotNull();
            this.Guidepost = guidepost;
            this.RenderingRoutedItem = renderingController;
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

        public IRenderingRouted RenderingRoutedItem { get; set; }

        public int Container
        {
            get { return this.Guidepost.Value; }
        }

        public int ContainerPriority { get; set; }
    }
        
}
