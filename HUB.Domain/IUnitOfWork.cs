using System;
using System.Threading.Tasks;

namespace HUB.Domain
{
    public interface IUnitOfWork
        : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }
}