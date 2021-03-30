using System;

namespace SuperNiceProject
{
    public interface IUnitOfWork : IDisposable
    {
        void Start();
        void Complete();
    }
}
