﻿using System;
using System.ComponentModel.Composition;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using NewCRM.Application.Interface;
using NewCRM.Dto.Dto;

namespace NewCRM.Web.Controllers.ControllerHelper
{

    public abstract class BaseController : Controller
    {
        [Import]
        protected IAppApplicationServices AppApplicationServices { get; set; }

        [Import]
        protected IDeskApplicationServices DeskApplicationServices { get; set; }

        [Import]
        protected IWallpaperApplicationServices WallpaperApplicationServices { get; set; }

        [Import]
        protected ISkinApplicationServices SkinApplicationServices { get; set; }

        [Import]
        protected IAccountApplicationServices AccountApplicationServices { get; set; }

        [Import]
        protected ISecurityApplicationServices SecurityApplicationServices { get; set; }


        /// <summary>
        /// 当前登陆的账户
        /// </summary>
        protected static AccountDto Account { get; set; }

        /// <summary>
        /// 当前用户的配置
        /// </summary>
        protected static ConfigDto AccountConfig { get; set; }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            String exceptionMessage = "";

            if (filterContext.Exception.Message.Length > 50)
            {
                exceptionMessage = filterContext.Exception.Message.Substring(0, 50) + "...";
            }
            else
            {
                exceptionMessage = filterContext.Exception.Message;
            }

            exceptionMessage = Regex.Replace(exceptionMessage, @"[\r\n]", "");

            if (filterContext.RequestContext.HttpContext.Request.HttpMethod.ToLower() != "post")
            {
                filterContext.Result = new ContentResult
                {
                    Content = @"<script>setTimeout(function(){window.top.ZENG.msgbox.show('" + exceptionMessage + "', 5,3000);},0)</script>",
                    ContentEncoding = Encoding.UTF8
                };
            }
            else
            {
                filterContext.Result = new JsonResult
                {
                    ContentEncoding = Encoding.UTF8,
                    Data = new { js = @"<script>setTimeout(function(){window.top.ZENG.msgbox.show('" + exceptionMessage + "', 5,3000);},0)</script>" }
                };
            }
        }
    }
}
