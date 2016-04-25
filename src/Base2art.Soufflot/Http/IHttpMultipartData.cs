namespace Base2art.Soufflot.Http
{
    using System.Collections.Generic;

    using Base2art.Collections;

    public interface IHttpMultipartData
    {
        IReadOnlyMultiMap<string, string> AsFormUrlEncoded();

        IKeyedCollection<string, IHttpFilePart> Files();
    }
}