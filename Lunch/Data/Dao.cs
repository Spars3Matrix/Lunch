using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Lunch.Data
{
    public abstract class Dao
    {   
        protected void Execute(Action<LunchDatabase> action) {
            using (LunchDatabase database = new LunchDatabase())
            {
                action(database);
            }
        }
        
        protected T Execute<T>(Func<LunchDatabase, T> action) {
            using (LunchDatabase database = new LunchDatabase())
            {
                return action(database);
            }
        }

        protected T FindEntity<T>(
            DbSet<T> collection, 
            Expression<Func<T, bool>> predicate) where T: class
        {
            return collection.FirstOrDefault(predicate);
        }

        protected IEnumerable<T> FindEntities<T>(
            DbSet<T> collection, 
            Expression<Func<T, bool>> predicate) where T: class
        {
            return collection.Where(predicate).ToList();
        }

        protected T AddOrUpdateEntity<T>(
            T entity, 
            DbSet<T> collection, 
            Expression<Func<T, bool>> predicate) where T: class
        {
            if (collection.Any(predicate)) collection.Update(entity);
            else collection.Add(entity);

            return entity;
        }

        protected void DeleteEntity<T, R>(
            T value, 
            DbSet<R> collection, 
            Expression<Func<R, bool>> predicate) where R: class
        {
            R entity = collection.FirstOrDefault(predicate);
            if (entity == null) return;
            collection.Remove(entity);
        }
    }
}