﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NewCRM.Application.Services.Interface;
using NewCRM.Dto;
using NewCRM.Infrastructure.CommonTools;
using NewCRM.Web.Controllers.ControllerHelper;
using NewLib.Validate;

namespace NewCRM.Web.Controllers
{
	public class AppManagerController : BaseController
	{
		private readonly IAppServices _appServices;

		public AppManagerController(IAppServices appServices)
		{
			_appServices = appServices;
		}

		#region 页面

		/// <summary>
		/// 首页
		/// </summary>
		[HttpGet]
		public async Task<ActionResult> Index()
		{
			ViewData["AppTypes"] = await _appServices.GetAppTypesAsync();
			ViewData["AppStyles"] = _appServices.GetAppStyles().ToList();
			ViewData["AppStates"] = _appServices.GetAppStates().Where(w => w.Name == "未审核" || w.Name == "已发布").ToList();

			return View();
		}

		/// <summary>
		/// app审核
		/// </summary>
		[HttpGet]
		public async Task<ActionResult> AppAudit(Int32 appId)
		{
			AppDto appResult = null;
			if(appId != 0)// 如果appId为0则是新创建app
			{
				appResult = await _appServices.GetAppAsync(appId);
				ViewData["AppState"] = appResult.AppAuditState;
			}

			ViewData["AppTypes"] = await _appServices.GetAppTypesAsync();

			return View(appResult);
		}

		#endregion

		/// <summary>
		/// 获取所有的app
		/// </summary>
		[HttpGet]
		public ActionResult GetApps(String searchText, Int32 appTypeId, Int32 appStyleId, String appState, Int32 pageIndex, Int32 pageSize)
		{
			var response = new ResponseModels<IList<AppDto>>();
			var result = _appServices.GetAccountApps(0, searchText, appTypeId, appStyleId, appState, pageIndex, pageSize, out var totalCount);

			foreach(var appDto in result)
			{
				appDto.IsCreater = appDto.AccountId == AccountId;
			}

			response.TotalCount = totalCount;
			response.IsSuccess = true;
			response.Message = "获取app列表成功";
			response.Model = result;

			return Json(response, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// app审核通过
		/// </summary>
		[HttpPost]
		public async Task<ActionResult> Pass(Int32 appId)
		{
			#region 参数验证	
			new Parameter().Validate(appId);
			#endregion

			var response = new ResponseModel();
			await _appServices.PassAsync(appId);
			response.IsSuccess = true;
			response.Message = "app审核通过";

			return Json(response);
		}

		/// <summary>
		/// app审核不通过
		/// </summary>
		[HttpPost]
		public async Task<ActionResult> DenyAsync(Int32 appId)
		{
			#region 参数验证	
			new Parameter().Validate(appId);
			#endregion

			var response = new ResponseModel();
			await _appServices.DenyAsync(appId);
			response.IsSuccess = true;
			response.Message = "app审核不通过";

			return Json(response);
		}

		/// <summary>
		/// 设置app为今日推荐
		/// </summary>
		[HttpPost]
		public async Task<ActionResult> Recommend(Int32 appId)
		{
			#region 参数验证	
			new Parameter().Validate(appId);
			#endregion

			var response = new ResponseModel();
			await _appServices.SetTodayRecommandAppAsync(appId);
			response.IsSuccess = true;
			response.Message = "设置成功";

			return Json(response);
		}

		/// <summary>
		/// 删除app
		/// </summary>
		[HttpPost]
		public async Task<ActionResult> RemoveApp(Int32 appId)
		{
			#region 参数验证	
			new Parameter().Validate(appId);
			#endregion

			var response = new ResponseModel();
			await _appServices.RemoveAppAsync(appId);
			response.IsSuccess = true;
			response.Message = "删除app成功";

			return Json(response);
		}


		/// <summary>
		/// 检查应用名称
		/// </summary>
		[HttpPost]
		public async Task<ActionResult> CheckAppName(String param)
		{
			#region 参数验证
			new Parameter().Validate(param);
			#endregion

			var result = await AccountServices.CheckAppNameAsync(param);
			return Json(!result ? new { status = "y", info = "" } : new { status = "n", info = "应用名称已存在" });
		}

		/// <summary>
		/// 检查应用Url
		/// </summary>
		[HttpPost]
		public async Task<ActionResult> CheckAppUrl(String param)
		{
			#region 参数验证
			new Parameter().Validate(param);
			#endregion

			var result = await AccountServices.CheckAppUrlAsync(param);
			return Json(!result ? new { status = "y", info = "" } : new { status = "n", info = "应用Url已存在" });
		}
	}
}