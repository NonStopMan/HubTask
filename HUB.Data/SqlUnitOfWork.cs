using HUB.Data;
using Microsoft.EntityFrameworkCore;
using NetCore.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Hub.Data
{
    public class SqlUnitOfWork : IQueryableUnitOfWork
    {

        private readonly PostContext _context;
        public SqlUnitOfWork(PostContext context)
        {
            _context = context;
        }

        public void AddOrUpdate<TEntity>(TEntity entity) where TEntity : class
        {
            var tracked = _context.Set<TEntity>().Find(_context.Entry(entity));
            if (tracked != null)
            {
                _context.Entry(tracked).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Added;
            }
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry(item).State = EntityState.Unchanged;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry(item).State = EntityState.Detached;
            _context.Entry(item).State = EntityState.Unchanged;
            _context.Entry(item).CurrentValues.Properties.Select(c => c.Name)
                .ToList()
                .ForEach(p => _context.Entry(item).Property(p).IsModified = true);
        }

        public void SetModified<TEntity>(TEntity item, string[] includedProperties) where TEntity : class
        {
            _context.Entry(item).State = EntityState.Detached;
            _context.Entry(item).State = EntityState.Unchanged;
            _context.Entry(item).CurrentValues.Properties.Select(c => c.Name)
                .Where(includedProperties.Contains)
                .ToList()
                .ForEach(p => _context.Entry(item).Property(p).IsModified = true);
        }

    }

}

