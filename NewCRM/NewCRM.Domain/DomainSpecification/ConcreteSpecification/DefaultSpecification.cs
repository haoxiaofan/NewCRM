﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NewCRM.Domain.Entitys;

namespace NewCRM.Domain.DomainSpecification.ConcreteSpecification
{

    /// <summary>
    /// 默认规约 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DefaultSpecification<T> : Specification<T> where T : DomainModelBase, IAggregationRoot
    {
        public override Expression<Func<T, Boolean>> Expression { get; }

        public override IList<Expression<Func<T, dynamic>>> OrderByExpressions { get; protected set; }


        public DefaultSpecification(Expression<Func<T, Boolean>> expression)
        {
            Expression = expression;
        }

        public DefaultSpecification()
        {
            Expression = arg => true;
        }


        public override void AddOrderByExpression(Expression<Func<T, dynamic>> expression)
        {
            if (OrderByExpressions == null)
            {
                OrderByExpressions = new List<Expression<Func<T, dynamic>>>();
            }
            OrderByExpressions.Add(expression);
        }

        public override void ResetOrderByExpressions()
        {
            OrderByExpressions?.Clear();
        }
    }
}
