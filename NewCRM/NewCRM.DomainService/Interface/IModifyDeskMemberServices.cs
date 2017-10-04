﻿using System;
using NewCRM.Domain.Entitys.System;

namespace NewCRM.Domain.Services.Interface
{
    public interface IModifyDeskMemberServices
    {
	    /// <summary>
	    /// 修改文件夹的信息
	    /// </summary>
	    /// <param name="accountId"></param>
	    /// <param name="memberName"></param>
	    /// <param name="memberIcon"></param>
	    /// <param name="memberId"></param>
	    void ModifyFolderInfo(Int32 accountId,String memberName, String memberIcon, Int32 memberId);

	    /// <summary>
	    /// 修改成员信息
	    /// </summary>
	    /// <param name="accountId"></param>
	    /// <param name="member"></param>
	    void ModifyMemberInfo(Int32 accountId, Member member);

	    /// <summary>
	    /// 移除用户的桌面app成员
	    /// </summary>
	    /// <param name="accountId"></param>
	    /// <param name="memberId"></param>
	    void RemoveMember(Int32 accountId, Int32 memberId);
    }
}