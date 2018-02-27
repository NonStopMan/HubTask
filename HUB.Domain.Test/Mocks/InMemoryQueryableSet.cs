using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Syngenta.BT.Domain.Tests.Mocks
{
    public class InMemoryQueryableSet<TEntity> : DbSet<TEntity>, IInMemoryQueryableSet where TEntity : class
    {
        private readonly IQueryable<TEntity> _queryableSet;
        private readonly HashSet<TEntity> _set;

        #region IEnumerable<TEntity> Members

        public virtual IEnumerator<TEntity> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        #endregion
        
        #region ctor

        public InMemoryQueryableSet()
        {
            _set = new HashSet<TEntity>();
            _queryableSet = _set.AsQueryable();
        }

        public InMemoryQueryableSet(IEnumerable<TEntity> entities)
        {
            _set = new HashSet<TEntity>(entities);
            _queryableSet = _set.AsQueryable();
        }

        #endregion

        #region IDbSet<TEntity> Members

        public virtual TEntity Add(TEntity entity)
        {
            _set.Add(entity);
            return entity;
        }

        public virtual TEntity Attach(TEntity entity)
        {
            _set.Add(entity);
            return entity;
        }

        public virtual TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            var element = Activator.CreateInstance<TDerivedEntity>();
            _set.Add(element);
            return element;
        }

        public virtual TEntity Create()
        {
            var element = Activator.CreateInstance<TEntity>();
            _set.Add(element);
            return element;
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _set.First();
        }

        public virtual ObservableCollection<TEntity> Local
        {
            get { return new ObservableCollection<TEntity>(_set); }
        }

        public virtual TEntity Remove(TEntity entity)
        {
            _set.Remove(entity);
            return entity;
        }

        #endregion

        #region IQueryable Members

        public virtual Type ElementType
        {
            get { return typeof (TEntity); }
        }

        public virtual Expression Expression
        {
            get { return _queryableSet.Expression; }
        }

        public virtual IQueryProvider Provider
        {
            get { return _queryableSet.Provider; }
        }

        #endregion
    }

    public interface IInMemoryQueryableSet
    {
    }
}