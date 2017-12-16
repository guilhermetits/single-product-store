using SingleProductStore.Business.Contract.Service;
using SingleProductStore.Data.Sql.Repository.Contract;
using SingleProductStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SingleProductStore.Business.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        #region .::Fields::.
        private readonly IRepository<T> repository;
        #endregion

        #region .::Ctor::.

        public BaseService(IRepository<T> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #endregion

        #region .::Properties::.

        public virtual IRepository<T> Repository
        {
            get { return repository; }
        }

        public virtual bool AutoSaveEnabled
        {
            get { return this.repository.AutoCommitEnabled; }
            set { this.repository.AutoCommitEnabled = value; }
        }

        #endregion

        #region .::Read Actions::.

        public virtual async Task<T> FindAsync(params object[] keyValues)
        {
            if (keyValues == null)
            {
                throw new ArgumentNullException("keyValues");
            }

            return await Repository.Find(keyValues);

        }

        public virtual async Task<IEnumerable<T>> GetAsync()
        {
            return await this.Repository.Get();

        }

        public virtual async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await this.Repository.Where(filterExpression);
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filterExpression, T defaultValue)
        {
            T result = await Repository.SingleOrDefault(filterExpression);

            if (result == null && defaultValue != null)
            {
                result = defaultValue;
            }
            return result;
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await SingleOrDefaultAsync(filterExpression, null);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await this.Repository.ExistsAsync(filterExpression);
        }
        #endregion

        #region .::Write Actions::.

        public virtual async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await this.Repository.Insert(entity);
        }

        public virtual async Task InsertRangeAsync(IEnumerable<T> entities, int batchSize)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("entities");
            }

            await this.Repository.InsertRange(entities, batchSize);
        }
        public virtual async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("entities");
            }

            await this.InsertRangeAsync(entities, 10);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            await this.Repository.Delete(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await this.Repository.Update(entity);
        }
        #endregion

        public virtual void Dispose()
        {
            //in case of the property been overrided
            if (this.repository.GetType().FullName != this.Repository.GetType().FullName)
            {
                this.repository.Dispose();
            }

            this.Repository.Dispose();

        }
    }
}
