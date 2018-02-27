using HUB.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HUB.Data
{
    public interface IQueryableUnitOfWork
        : IUnitOfWork
    {
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;
        void AddOrUpdate<TEntity>(TEntity entity) where TEntity : class;
        void Attach<TEntity>(TEntity item) where TEntity : class;
        void SetModified<TEntity>(TEntity item) where TEntity : class;
        void SetModified<TEntity>(TEntity item, string[] includedProperties) where TEntity : class;
    }
}