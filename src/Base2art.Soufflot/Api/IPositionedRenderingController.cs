namespace Base2art.Soufflot.Api
{
    public interface IPositionedRenderingRouted
    {
        IRenderingRouted RenderingRoutedItem { get; }

        int Container { get; }

        int ContainerPriority { get; }
    }
}
