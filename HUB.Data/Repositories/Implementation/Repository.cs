using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HUB.Data.Resources;
using HUB.Domain;
using HUB.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HUB.Data.Repositories.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        protected DbSet<TEntity> GetSet()
        {
            return _unitOfWork.CreateSet<TEntity>();
        }

        #region Constructor

        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public Task<TEntity> GetAsync(object[] keyValues)
        {
            //IDbSet don`t have a FindAsync, a work around it to cast to Dbset losing the benifits of abstraction
            return keyValues != null ? ((DbSet<TEntity>)GetSet()).FindAsync(keyValues) : null;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return GetSet();
        }

        public virtual void Add(TEntity item)
        {
            if (item != null)
            {
                GetSet().Add(item); // add new item in this set
            }
            else
            {
                //LoggerFactory.Info(string.Format(Messages.info_CannotAddNullEntity, typeof(TEntity)));
            }
        }

        public virtual void AddOrUpdate(TEntity item)
        {
            _unitOfWork.AddOrUpdate(item);
        }


        public virtual void Delete(TEntity item)
        {
            if (item != null)
            {
                //attach item if not exist
                _unitOfWork.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
            else
            {
               // LoggerFactory.CreateLogger().Info(string.Format(Messages.info_CannotRemoveNullEntity, typeof(TEntity)));
            }
        }

        public virtual void TrackItem(TEntity item)
        {
            if (item != null)
                _unitOfWork.Attach(item);
            else
            {
               // ILogger.Info(string.Format(Messages.info_CannotTrackNullEntity, typeof(TEntity)));
            }
        }

        public virtual void Update(TEntity item)
        {
            if (item != null)
            {
                //this operation also attach item in object state manager
                _unitOfWork.SetModified(item);
            }
            else
            {
                //LoggerFactory.CreateLogger().Info(string.Format(Messages.info_CannotModifyNullEntity, typeof(TEntity)));
            }
        }

        public virtual void Update(TEntity item, string[] includedProperties)
        {
            if (item != null)
            {
                //this operation also attach item in object state manager
                _unitOfWork.SetModified(item, includedProperties);
            }
            else
            {
                //LoggerFactory.CreateLogger()
                //    .Info(string.Format(Messages.info_CannotModifyNullEntity, typeof(TEntity)));
            }
        }

        #endregion
    }
}