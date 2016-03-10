﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewCRM.Domain.DomainModel.Security;
using NewCRM.Infrastructure.Repositories.RepositoryProvide;

namespace NewCRM.Infrastructure.Repositories.Configuration.Security.Imp
{
    internal partial class RoleConfiguration : EntityTypeConfiguration<Role>, IEntityMapper
    {
        public RoleConfiguration()
        {
            HasKey(a => a.Id);

            HasMany(a => a.Apps).
                WithMany(a => a.Roles).
                Map(a => a.ToTable("RoleApp").MapLeftKey("RoleId").MapRightKey("AppId"));
        }
        public void RegistTo(ConfigurationRegistrar configurations) { configurations.Add(this); }
    }
}