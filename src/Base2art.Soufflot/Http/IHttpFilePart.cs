namespace Base2art.Soufflot.Http
{
    public interface IHttpFilePart
    {
        string ContentType { get; }
        
        string Key { get; }
        
        string FileName { get; }
        
        System.IO.FileInfo File { get; }
    }
}
