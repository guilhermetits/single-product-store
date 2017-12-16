using Microsoft.EntityFrameworkCore;
using SingleProductStore.Data.Sql.Context;
using SingleProductStore.Data.Sql.Repository.Contract;
using SingleProductStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        public IDbContext Context => db;
        protected DbSet<T> Entities => entitySet = entitySet ?? db.Set<T>();
        #endregion

        #region .::Ctor::.

        public BaseRepository(IDbContext context)
        {
            this.AutoCommitEnabled = true;
            this.SetOrChangeContext(context);
        }

        #endregion

        #region .::Read Actions::.

        public async Task<T> Find(params object[] keyValues)
        {
            return await this.Entities?.FindAsync(keyValues);
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await this.Entities.ToListAsync();
        }
         
        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> filterExpression)
        {
            return this.Entities?.Where(filterExpression);
        }

        public async Task<T> SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filterExpression)
        {
            return await this.Entities?.SingleOrDefaultAsync(filterExpression);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await this.Entities?.AnyAsync(filterExpression);
        }

        #endregion

        #region .::Write Actions::.

        public async Task Insert(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = null;
            entity.Active = true;

            this.Entities.Add(entity);
            if (this.AutoCommitEnabled)
            {
                try
                {
                    await db.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    ex.ToString();
                    throw;
                }
            }
        }

        public async Task InsertRange(IEnumerable<T> entities, int? batchSize = null)
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
                        await this.Context.SaveChangesAsync();
                    }
                }
                else
                {
                    int i = 1;
                    bool saved = false;
                    foreach (var entity in entities)
                    {
                        await this.Insert(entity);
                        saved = false;
                        if (i % batchSize.Value == 0)
                        {
                            Task task = null;
                            if (task != null)
                            {
                                task.Wait();
                                task = null;
                            }
                            if (this.AutoCommitEnabled)
                            {
                                task = Context.SaveChangesAsync();
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
                            await this.Context.SaveChangesAsync();
                        }
                    }
                }
            }
        }

        public async Task Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.Entry(entity).Property(t => t.CreatedAt).IsModified = false;
            db.Entry(entity).Property(t => t.UpdatedAt).CurrentValue = DateTime.UtcNow;
            if (this.AutoCommitEnabled)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        public async Task Delete(T entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
            {
                this.Entities.Attach(entity);
            }
            this.Entities.Remove(entity);

            if (this.AutoCommitEnabled)
            {
                await this.Context.SaveChangesAsync();
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

        public async Task<int> Commit()
        {
            return await this.Context.SaveChangesAsync();
        }

        #endregion
    }
}
