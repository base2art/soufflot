namespace Base2art.Soufflot.Api
{
    public interface IComponentResolver
    {
        T Resolve<T>(IClass<T> type, bool returnNullOnErrorOrNotFound);
        
        T[] ResolveAll<T>(IClass<T> type, bool returnNullOnErrorOrNotFound);
    }
}
