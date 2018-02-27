using HUB.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Syngenta.BT.Domain.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HUB.Domain.Tests.Mocks
{
    public sealed class MockDbContext
    {
        #region Private fields

        private Dictionary<Type, object> _internalSets;

        #endregion

        #region Ctor

        public MockDbContext()
        {
            InitializeStructure();
            InitializeData();
            
        }

        #endregion

        #region Private Methods

        private void InitializeData()
        {
            var entities = CreateEntityList();
            foreach (var entity in entities)
            {
                Entities.Add(entity);
            }
        }

        private void InitializeStructure()
        {
            _internalSets = new Dictionary<Type, object>();
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.PropertyType.Name != "IDbSet`1" || propertyInfo.GetValue(this, null) != null)
                    continue;
                var classType = typeof(InMemoryQueryableSet<>);
                var typeParams = propertyInfo.PropertyType.GetGenericArguments();
                var constructedType = classType.MakeGenericType(typeParams);

                var propertyValue =
                    Activator.CreateInstance(constructedType);
                propertyInfo.SetValue(this, propertyValue, null);
                _internalSets.Add(typeParams[0], propertyValue);
            }
        }

        private IEnumerable<Entity> CreateEntityList()
        {
            var j = 60;
            for (var i = 0; i < 50; i++, j--)
            {
                yield return new Entity { Id = i, Name = "name" + j };
            }
        }

        #endregion

        #region Public Properties

        //#IDbSet-Don'tRemove#
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Post> Posts { get; set; }


        #endregion

        #region Public Methods

        public DbSet<T> Set<T>() where T : class
        {
            if (typeof(T) == typeof(Post)) return this.Posts as DbSet<T>;
            
            return _internalSets[typeof(T)] as DbSet<T>;
        }

        public void Attach<T>(T entity) where T : class
        {
            if (typeof(T) == typeof(Post))
            {
                var set = this.Posts as DbSet<T>;
                set.Add(entity);
            }
           
        }

        #endregion
    }
}