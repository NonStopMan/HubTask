using System;
using System.Linq;
using HUB.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HUB.Data.Repositories.Implementation
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class
    {
        #region Members

        protected readonly IQueryableUnitOfWork UnitOfWork;

        #endregion

        #region Constructor

        public ReadOnlyRepository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            UnitOfWork = unitOfWork;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }

        #endregion

        #region Protected Methods

        protected DbSet<TEntity> GetSet()
        {
            return UnitOfWork.CreateSet<TEntity>();
        }

        #endregion

        #region IRepository Members

        public virtual IQueryable<TEntity> GetAll()
        {
            return GetSet().AsNoTracking();
        }
        public virtual IQueryable<TEntity> GetAll(string includedProperty)
        {
            return GetSet().AsNoTracking().Include(includedProperty);
        }

        #endregion
    }
}