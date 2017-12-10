using SingleProductStore.Data.Sql.Context;
using SingleProductStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SingleProductStore.Data.Sql.Repository.Contract
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        #region .::Properties::.
        IDbContext Context { get; }
        bool AutoCommitEnabled { get; set; }
        #endregion

        #region .::Read Actions::.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Gel all Entitys of the type
        /// </summary>
        /// <returns>IEnumerable of TEntity</returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filterExpression);
        #endregion

        #region .::Write Actions::.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void InsertRange(IEnumerable<TEntity> entities, int? batchSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        #endregion

        #region .::Helpers::.
        void Commit();

        #endregion
    }
}
