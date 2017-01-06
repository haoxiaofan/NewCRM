﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCRM.Repository.DataBaseProvider.EF.Event
{
    internal interface ICacheModifyEvent
    {
        event EventHandler OnCacheModify;

        void CacheModify();
    }
}