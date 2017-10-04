﻿using System;
using System.Web;
using System.Web.Mvc;
using NewCRM.Application.Services.Interface;
using NewCRM.Dto.Dto;
using NewCRM.Infrastructure.CommonTools;
using NewCRM.Infrastructure.CommonTools.CustomHelper;
using NewCRM.Web.Controllers.ControllerHelper;
using Newtonsoft.Json;

namespace NewCRM.Web.Controllers
{
	public class LoginController : BaseController
	{
		private readonly IAccountApplicationServices _accountApplicationServices;


		public LoginController(IAccountApplicationServices accountApplicationServices)
		{
			_accountApplicationServices = accountApplicationServices;
		}


		// GET: Login
		public ActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// 登陆
		/// </summary>
		/// <param name="accountName"></param>
		/// <param name="passWord"></param>
		/// <param name="isRememberPasswrod"></param>
		/// <returns></returns>
		public ActionResult Landing(String accountName, String passWord, Boolean isRememberPasswrod = false)
		{
			var response = new ResponseModel<AccountDto>();

			#region 参数验证
			Parameter.Validate(accountName).Validate(passWord);
			#endregion

			var account = _accountApplicationServices.Login(accountName, passWord);
			if (account != null)
			{
				response.Message = "登陆成功";
				response.Model = account;
				response.IsSuccess = true;

				Response.SetCookie(new HttpCookie("Account")
				{
					Name = account.Id.ToString(),
					Value = JsonConvert.SerializeObject(account),
					Expires = isRememberPasswrod ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(30)
				});
			}

			return Json(response);
		}
	}
}