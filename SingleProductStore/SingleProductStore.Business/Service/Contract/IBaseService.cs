using SingleProductStore.Data.Sql.Repository.Contract;
using SingleProductStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SingleProductStore.Business.Contract.Service
{
    public interface IBaseService<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IRepository<TEntity> Repository { get; }
        bool AutoSaveEnabled { get; set; }

        #region .::Read Options::.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// Gel all Entitys of the type
        /// </summary>
        /// <returns>IEnumerable of TEntity</returns>
        Task<IEnumerable<TEntity>> GetAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filterExpression,
            TEntity defaultValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filterExpression);
        #endregion

        #region .::Write Actions::.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        Task InsertRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task DeleteAsync(TEntity entity);
        #endregion

    }
}