﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//	   如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

using NewCRM.Infrastructure.Repositories;
using NewCRM.Domain.DomainModel;
using NewCRM.Infrastructure.Repositories.RepositoryProvide;
using NewCRM.Domain.DomainModel.Account;
using NewCRM.Domain.DomainModel.Security;
using NewCRM.Domain.DomainModel.System;


namespace NewCRM.Infrastructure.Repositories.Configurations
{
    /// <summary>
    /// 实体类-数据表映射——应用类型
    /// </summary>    
    internal partial class AppTypeConfiguration : EntityTypeConfiguration<AppType>, IEntityMapper
    {
        /// <summary>
        /// 实体类-数据表映射构造函数——应用类型
        /// </summary>
        public AppTypeConfiguration()
        {
            AppTypeConfigurationAppend();
        }
        
        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void AppTypeConfigurationAppend();
        
        /// <summary>
        /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
        /// </summary>
        /// <param name="configurations">实体映射配置注册器</param>
        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
