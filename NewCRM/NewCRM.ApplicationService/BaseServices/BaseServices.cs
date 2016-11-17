﻿using System;
using System.ComponentModel.Composition;
using NewCRM.Domain.DomainQuery.Query;
using NewCRM.Domain.DomainSpecification.Factory;
using NewCRM.Domain.Entitys.Account;
using NewCRM.Domain.Factory;
using NewCRM.Domain.Interface.BoundedContext.Account;
using NewCRM.Domain.Interface.BoundedContext.App;
using NewCRM.Domain.Interface.BoundedContext.Desk;
using NewCRM.Domain.Interface.BoundedContext.Wallpaper;
using NewCRM.Domain.UnitWork;
using NewCRM.Infrastructure.CommonTools.CustomHelper;

namespace NewCRM.Application.Services.BaseServices
{

    public abstract class BaseServices
    {
        [Import]
        protected IUnitOfWork UnitOfWork { get; set; }

        [Import]
        protected IAccountContext AccountContext { get; set; }

        [Import]
        protected IAppContext AppContext { get; set; }

        [Import]
        protected IDeskContext DeskContext { get; set; }

        [Import]
        protected IWallpaperContext WallpaperContext { get; set; }


        /// <summary>
        /// 仓储工厂
        /// </summary>
        [Import]
        protected RepositoryFactory Repository { get; set; }

        /// <summary>
        /// 查询工厂
        /// </summary>
        [Import]
        protected QueryFactory QueryFactory { get; set; }

        /// <summary>
        /// 规约工厂
        /// </summary>
        [Import]
        protected SpecificationFactory SpecificationFactory { get; set; }

        /// <summary>
        /// 参数验证
        /// </summary>
        protected static Parameter ValidateParameter => new Parameter();

        /// <summary>
        /// 获取登陆的账户
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        internal Account GetAccountInfoService(Int32 accountId)
        {
            return QueryFactory.Create<Account>().FindOne(SpecificationFactory.Create<Account>(account => account.Id == accountId));
        }
    }
}
