﻿using SingleProductStore.Data.Sql.Context;
using SingleProductStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        Task<TEntity> Find(params object[] keyValues);

        /// <summary>
        /// Gel all Entitys of the type
        /// </summary>
        /// <returns>IEnumerable of TEntity</returns>
        Task<IEnumerable<TEntity>> Get();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filterExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> filterExpression);
        #endregion

        #region .::Write Actions::.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task Insert(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        Task InsertRange(IEnumerable<TEntity> entities, int? batchSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task Update(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task Delete(TEntity entity);
        #endregion

        #region .::Helpers::.
        Task<int> Commit();

        #endregion
    }
}
