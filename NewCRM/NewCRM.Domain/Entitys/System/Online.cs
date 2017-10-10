﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewCRM.Domain.Entitys.System
{
    [Description("在线人数"), Serializable]
    public partial class Online : DomainModelBase, IAggregationRoot
    {
        #region public property

        /// <summary>
        /// Ip地址
        /// </summary>
        [Required()]
        public String IpAddress { get; private set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required()]
        public Int32 AccountId { get; private set; }
        #endregion

        #region ctor

        /// <summary>
        /// 实例化一个在线状态的对象
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="accountId"></param>
        public Online(String ipAddress, Int32 accountId) : this()
        {
            IpAddress = ipAddress;
            AccountId = accountId;
        }

        public Online() { AddTime = DateTime.Now; }


        #endregion
    }
}
