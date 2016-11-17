﻿using System.Linq;
using NewCRM.Domain.DomainSpecification;
using NewCRM.Domain.Entitys;

namespace NewCRM.Domain.Repositories
{
    public interface IDomainModelQueryProvider
    {
        IQueryable<T> Query<T>(Specification<T> selector) where T : DomainModelBase, IAggregationRoot;
    }
}
