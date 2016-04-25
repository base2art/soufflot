namespace Base2art.Soufflot.Mvc
{
    public interface IPositionedRenderingController
    {
        IRenderingController RenderingController { get; }

        int Container { get; }

        int ContainerPriority { get; }
    }
}