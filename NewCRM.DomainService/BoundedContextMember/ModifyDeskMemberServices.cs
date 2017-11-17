﻿using System;
using System.Collections.Generic;
using System.Linq;
using NewCRM.Domain.Entitys.System;
using NewCRM.Domain.Services.Interface;
using NewCRM.Domain.ValueObject;
using NewCRM.Domain.Repositories.IRepository.System;

namespace NewCRM.Domain.Services.BoundedContextMember
{

    public sealed class ModifyDeskMemberServices : BaseServiceContext, IModifyDeskMemberServices
    {
        private readonly IDeskRepository _deskRepository;
        private readonly IAppRepository _appRepository;

        public ModifyDeskMemberServices(IDeskRepository deskRepository, IAppRepository appRepository)
        {
            _deskRepository = deskRepository;
            _appRepository = appRepository;
        }

        public void ModifyFolderInfo(Int32 accountId, String memberName, String memberIcon, Int32 memberId)
        {
            ValidateParameter.Validate(accountId).Validate(memberName).Validate(memberIcon).Validate(memberId);

            foreach (var desk in GetDesks(accountId))
            {
                var memberResult = GetMember(memberId, desk);
                if (memberResult != null)
                {
                    memberResult.ModifyName(memberName).ModifyIcon(memberIcon);
                    _deskRepository.Update(desk);

                    break;
                }
            }

        }

        public void ModifyMemberIcon(Int32 accountId, Int32 memberId, String newIcon)
        {
            ValidateParameter.Validate(accountId).Validate(memberId).Validate(newIcon);

            foreach (var desk in GetDesks(accountId))
            {
                var result = desk.Members.FirstOrDefault(a => a.Id == memberId);
                if (result != null)
                {
                    result.ModifyIcon(newIcon);
                    _deskRepository.Update(desk);
                    break;
                }
            }
        }

        public void ModifyMemberInfo(Int32 accountId, Member member)
        {
            ValidateParameter.Validate(accountId).Validate(member);

            foreach (var desk in GetDesks(accountId))
            {
                var memberResult = GetMember(member.Id, desk);
                if (memberResult != null)
                {
                    memberResult.ModifyIcon(member.IconUrl)
                    .ModifyName(member.Name)
                    .ModifyWidth(member.Width)
                    .ModifyHeight(member.Height)
                    .ModifyIsResize(member.IsResize)
                    .ModifyIsOpenMax(member.IsOpenMax)
                    .ModifyIsFlash(member.IsFlash);

                    _deskRepository.Update(desk);

                    break;
                }
            }
        }

        public void RemoveMember(Int32 accountId, Int32 memberId)
        {
            ValidateParameter.Validate(accountId).Validate(memberId);

            var desks = GetDesks(accountId);
            App appResult = null;
            foreach (var desk in desks)
            {
                var memberResult = GetMember(memberId, desk);
                if (memberResult != null)
                {
                    if (memberResult.MemberType == MemberType.Folder)
                    {
                        //移除文件夹中的内容
                        foreach (var desk1 in desks)
                        {
                            desk1.Members.Where(d => d.FolderId == memberId).ToList().ForEach(m => m.Remove());
                        }
                    }
                    else
                    {
                        appResult = DatabaseQuery.FindOne(FilterFactory.Create<App>(app => app.Id == memberResult.AppId));
                        appResult.SubtractUseCount();
                        appResult.SubtractStar(accountId);
                    }

                    memberResult.Remove();
                    _deskRepository.Update(desk);
                    if (appResult != null)
                    {
                        _appRepository.Update(appResult);
                    }

                    break;
                }
            }

        }
    }
}