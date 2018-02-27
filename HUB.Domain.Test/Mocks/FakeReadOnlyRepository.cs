using System.Linq;
using HUB.Data;
using HUB.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HUB.Domain.Tests.Mocks
{
    public class FakeReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        #region Members

        private readonly IQueryableUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public FakeReadOnlyRepository(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository<TEntity> Members

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _unitOfWork.CreateSet<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> GetAll(string navigationProperty)
        {
            return _unitOfWork.CreateSet<TEntity>().AsNoTracking().Include(navigationProperty);
        }

        #endregion
    }
}