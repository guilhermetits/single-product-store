using Microsoft.EntityFrameworkCore;
using SingleProductStore.Data.Sql.Context;
using SingleProductStore.Data.Sql.Repository.Contract;
using SingleProductStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingleProductStore.Data.Sql.Repository
{
    public abstract class BaseRepository<T> : IRepository<T>
         where T : BaseEntity, new()
    {
        #region .::Fields::.

        protected IDbContext db;
        private DbSet<T> entitySet;

        #endregion

        #region .::Properties::.

        public bool AutoCommitEnabled { get; set; }
        public IDbContext Context
        {
            get
            {
                return db;
            }
        }
        protected DbSet<T> Entities
        {
            get
            {
                if (entitySet == null)
                {
                    entitySet = db.Set<T>();
                }
                return entitySet as DbSet<T>;
            }
        }
        #endregion

        #region .::Ctor::.

        public BaseRepository(IDbContext context)
        {
            this.AutoCommitEnabled = true;
            this.SetOrChangeContext(context);
        }

        #endregion

        #region .::Read Actions::.

        public T Find(params object[] keyValues)
        {

            return this.Entities?.Find(keyValues);
        }

        public IEnumerable<T> Get()
        {
            return this.Entities;
        }

        public IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> filterExpression)
        {
            return this.Entities?.Where(filterExpression);
        }

        public T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filterExpression)
        {
            return this.Entities?.SingleOrDefault(filterExpression);
        }

        #endregion

        #region .::Write Actions::.

        public void Insert(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = null;
            entity.Active = true;

            this.Entities.Add(entity);
            if (this.AutoCommitEnabled)
            {
                try
                {
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    ex.ToString();
                    throw;
                }
            }
        }

        public void InsertRange(IEnumerable<T> entities, int? batchSize = null)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            if (entities.Any())
            {
                if (!batchSize.HasValue)
                {
                    this.Entities.AddRange(entities);
                    if (this.AutoCommitEnabled)
                    {
                        this.Context.SaveChanges();
                    }
                }
                else
                {
                    int i = 1;
                    bool saved = false;
                    foreach (var entity in entities)
                    {
                        this.Insert(entity);
                        saved = false;
                        if (i % batchSize.Value == 0)
                        {
                            if (this.AutoCommitEnabled)
                            {
                                Context.SaveChanges();
                            }
                            i = 0;
                            saved = true;
                        }
                        i++;
                    }

                    if (!saved)
                    {
                        if (this.AutoCommitEnabled)
                        {
                            this.Context.SaveChanges();
                        }
                    }
                }
            }
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.Entry(entity).Property(t => t.CreatedAt).IsModified = false;
            db.Entry(entity).Property(t => t.UpdatedAt).CurrentValue = DateTime.UtcNow;
            if (this.AutoCommitEnabled)
            {
                this.Context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
            {
                this.Entities.Attach(entity);
            }
            this.Entities.Remove(entity);

            if (this.AutoCommitEnabled)
            {
                this.Context.SaveChanges();
            }

        }
        #endregion

        #region .::IDisposable::.
        public void Dispose()
        {
            Context.Dispose();
        }
        #endregion

        #region .::Helpers::.

        protected void SetOrChangeContext(IDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context", "The context can't be null");
            }

            entitySet = context.Set<T>();

            this.db = context;
        }

        public void Commit()
        {
            this.Context.SaveChanges();
        }

        #endregion
    }
}
