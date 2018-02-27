using System;
using System.Linq;

namespace HUB.Domain.Repositories
{
    public interface IReadOnlyRepository<out TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(string navigationProperty);

    }
}