﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NewCRM.Application.Services.Interface;
using NewCRM.Domain.ValueObject;
using NewCRM.Dto;
using NewCRM.Infrastructure.CommonTools;
using NewCRM.Infrastructure.CommonTools.CustomHelper;
using NewCRM.Web.Controllers.ControllerHelper;
using NewLib;
using NewLib.Validate;
using Newtonsoft.Json;
using Nito.AsyncEx;

namespace NewCRM.Web.Controllers
{
	public class DesktopController: BaseController
	{
		private readonly IDeskServices _deskServices;

		public DesktopController(IDeskServices deskServices)
		{
			_deskServices = deskServices;
		}

		#region 页面

		/// <summary>
		/// 首页
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult> Index()
		{
			ViewBag.Title = "桌面";
			if (Request.Cookies["memberID"] != null)
			{
				var account = await AccountServices.GetAccountAsync(AccountId);
				account.AccountFace = ProfileManager.FileUrl + account.AccountFace;
				ViewData["Account"] = account;
				ViewData["AccountConfig"] = await AccountServices.GetConfigAsync(account.Id);
				ViewData["Desks"] = (await AccountServices.GetConfigAsync(account.Id)).DefaultDeskCount;

				return View();
			}

			return RedirectToAction("Login", "Desktop");
		}

		/// <summary>
		/// 首页
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Login()
		{
			var accountId = Request.Cookies["memberID"];
			if (accountId != null)
			{
				return RedirectToAction("Index", "Desktop");
			}

			return View();
		}


		#endregion


		/// <summary>
		/// 登陆
		/// </summary>
		[HttpPost]
		public async Task<ActionResult> Landing(LoginParameter loginParameter)
		{
			#region 参数验证
			new Parameter().Validate(loginParameter);
			#endregion

			var response = new ResponseModel<AccountDto>();

			var account = await AccountServices.LoginAsync(loginParameter.Name, loginParameter.Password, Request.ServerVariables["REMOTE_ADDR"]);
			if (account != null)
			{
				response.Message = "登陆成功";
				response.IsSuccess = true;
				Response.Cookies.Add(new HttpCookie("memberID")
				{
					Value = account.Id.ToString(),
					Expires = loginParameter.IsRememberPasswrod ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(30)
				});

				Response.Cookies.Add(new HttpCookie("Account")
				{
					Value = JsonConvert.SerializeObject(new { AccountFace = ProfileManager.FileUrl + account.AccountFace, account.Name }),
					Expires = loginParameter.IsRememberPasswrod ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(30)
				});
			}
			return Json(response);
		}

		/// <summary>
		/// 解锁屏幕
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult> UnlockScreen(String unlockPassword)
		{
			#region 参数验证
			new Parameter().Validate(unlockPassword);
			#endregion

			var response = new ResponseModel();
			var result = await AccountServices.UnlockScreenAsync(AccountId, unlockPassword);
			if (result)
			{
				response.IsSuccess = true;
			}

			return Json(response, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 账户登出
		/// </summary>
		[HttpPost]
		public async Task<ActionResult> Logout()
		{
			await AccountServices.LogoutAsync(AccountId);
			InternalLogout();
			return new EmptyResult();
		}

		/// <summary>
		/// 初始化皮肤
		/// </summary>
		[HttpGet]
		public async Task<ActionResult> GetSkin()
		{
			var response = new ResponseModel<String>();
			var skinName = (await AccountServices.GetConfigAsync(AccountId)).Skin;
			response.IsSuccess = true;
			response.Model = skinName;
			response.Message = "初始化皮肤成功";

			return Json(response, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 初始化壁纸
		/// </summary>
		[HttpGet]
		public async Task<ActionResult> GetWallpaper()
		{
			var response = new ResponseModel<ConfigDto>();
			var result = await AccountServices.GetConfigAsync(AccountId);

			if (result.IsBing)
			{
				result.WallpaperSource = WallpaperSource.Bing.ToString().ToLower();
				result.WallpaperUrl = AsyncContext.Run(BingHelper.GetEverydayBackgroundImageAsync);
			}

			response.IsSuccess = true;
			response.Message = "初始化壁纸成功";
			response.Model = result;

			return Json(response, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 初始化应用码头
		/// </summary>
		[HttpGet]
		public async Task<ActionResult> GetDockPos()
		{
			var response = new ResponseModel<String>();
			var result = (await AccountServices.GetConfigAsync(AccountId)).DockPosition;
			response.IsSuccess = true;
			response.Message = "初始化应用码头成功";
			response.Model = result;

			return Json(response, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 获取我的应用
		/// </summary>
		[HttpGet]
		public async Task<ActionResult> GetAccountDeskMembers()
		{
			var response = new ResponseModel<IDictionary<String, IList<dynamic>>>();
			var result = await _deskServices.GetDeskMembersAsync(AccountId);
			response.IsSuccess = true;
			response.Message = "获取我的应用成功";
			response.Model = result;

			return Json(response, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 获取用户头像
		/// </summary>
		[HttpGet]
		public async Task<ActionResult> GetAccountFace()
		{
			var response = new ResponseModel<String>();
			var result = (await AccountServices.GetConfigAsync(AccountId)).AccountFace;
			response.IsSuccess = true;
			response.Message = "获取用户头像成功";
			response.Model = ProfileManager.FileUrl + result;

			return Json(response, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 创建一个窗口
		/// </summary>
		[HttpGet]
		public async Task<ActionResult> CreateWindow(Int32 id, String type)
		{

			#region 参数验证
			new Parameter().Validate(id).Validate(type);
			#endregion

			var response = new ResponseModel<dynamic>();
			var internalMemberResult = type == "folder" ? await _deskServices.GetMemberAsync(AccountId, id, true) : await _deskServices.GetMemberAsync(AccountId, id);
			response.IsSuccess = true;
			response.Message = "创建一个窗口成功";
			response.Model = new
			{
				type = internalMemberResult.MemberType.ToLower(),
				memberId = internalMemberResult.Id,
				appId = internalMemberResult.AppId,
				name = internalMemberResult.Name,
				icon = internalMemberResult.IsIconByUpload ? ProfileManager.FileUrl + internalMemberResult.IconUrl : internalMemberResult.IconUrl,
				width = internalMemberResult.Width,
				height = internalMemberResult.Height,
				isOnDock = internalMemberResult.IsOnDock,
				isDraw = internalMemberResult.IsDraw,
				isOpenMax = internalMemberResult.IsOpenMax,
				isSetbar = internalMemberResult.IsSetbar,
				url = internalMemberResult.AppUrl,
				isResize = internalMemberResult.IsResize
			};

			return Json(response, JsonRequestBehavior.AllowGet);
		}
	}
}