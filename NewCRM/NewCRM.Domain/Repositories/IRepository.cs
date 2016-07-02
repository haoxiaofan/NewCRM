﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NewCRM.Domain.Entities.DomainModel;

namespace NewCRM.Domain.Entities.Repositories
{
    /// <summary>
    /// 定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="T">动态实体类型</typeparam>
    public interface IRepository<T> where T : DomainModelBase, IAggregationRoot
    {

        #region 属性


        IQueryable<T> Entities { get; }

        #endregion

        #region 公共方法



        void Add(T entity, Boolean isSave = true);

        void Add(IEnumerable<T> entities, Boolean isSave = true);

        void Remove(Int32 id, Boolean isSave = true);

        void Remove(T entity, Boolean isSave = true);

        void Remove(IEnumerable<T> entities, Boolean isSave = true);

        void Remove(Expression<Func<T, Boolean>> predicate, Boolean isSave = true);

        void Update(T entity, Boolean isSave = true);
        void Update(Expression<Func<T, Boolean>> propertyExpression, T entity, Boolean isSave = true);


        #endregion
    }
}