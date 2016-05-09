namespace Base2art.Soufflot.Api.Fixtures
{
    using Base2art.Soufflot.Mvc;

    public class PositionedRenderingController : IPositionedRenderingRouted
    {
        public IRenderingRouted RenderingRoutedItem { get; set; }

        public int Container { get; set; }

        public int ContainerPriority { get; set; }
    }
}
