using HUB.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HUB.Domain.Tests.Mocks
{
    public class FakeUnitOfWork : IQueryableUnitOfWork
    {
        public FakeUnitOfWork()
        {
            Context = new MockDbContext();
        }

        public MockDbContext Context { get; }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        #region IQueryableUnitOfWork Members

        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        public void AddOrUpdate<TEntity>(TEntity entity) where TEntity : class
        {
            var set = Context.Set<TEntity>();
            var existingentity =
                set.FirstOrDefault(
                    e =>
                        typeof(TEntity).GetProperty("Id") != null &&
                        (Guid)typeof(TEntity).GetProperty("Id", typeof(Guid)).GetValue(e, null) ==
                        (Guid)typeof(TEntity).GetProperty("Id", typeof(Guid)).GetValue(entity, null));
            if (existingentity != null)
            {
                set.Remove(existingentity);
                set.Add(entity);
            }
            else set.Add(entity);
        }

        public void AddOrUpdateListBasedOnNonKeyUniqueIdentifier<TEntity>(List<TEntity> entities,
            Expression<Func<TEntity, object>> identifierExpression) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            Context.Attach(item);
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            var set = Context.Set<TEntity>();
            var entity =
                set.FirstOrDefault(
                    e =>
                        typeof(TEntity).GetProperty("Id") != null &&
                        (Guid)typeof(TEntity).GetProperty("Id", typeof(Guid)).GetValue(e, null) ==
                        (Guid)typeof(TEntity).GetProperty("Id", typeof(Guid)).GetValue(item, null));
            if (entity != null)
            {
                set.Remove(entity);
                set.Add(item);
            }
            else set.Add(item);
        }

        public void SetModified<TEntity>(TEntity item, string[] includedProperties) where TEntity : class
        {
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            var set = Context.Set<TEntity>();
            set.Remove(original);
            set.Add(current);
        }

        public Task DeleteWhere<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task UpdateWhere<TEntity>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateFactory) where TEntity : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUnitOfWork Members

        public int Commit()
        {
            // do nothin
            return 0;
        }

        public async Task<int> CommitAsync()
        {
            return await Task.Run(() => 0);
        }

        DbSet<TEntity> IQueryableUnitOfWork.CreateSet<TEntity>()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}