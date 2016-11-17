﻿using System.ComponentModel;

namespace NewCRM.Domain.ValueObject
{
    public enum AccountType
    {
        [Description("普通用户")]
        User = 1,

        [Description("管理员")]
        Admin = 2
    }
}
