namespace Base2art.Soufflot.CommandRunner
{
    using System;

    public interface IApplicationRunner : IDisposable
    {
        void Run(
            string rootDir,
            string binPath,
            string currentDomainBin,
            int? portNumber);
    }
}
