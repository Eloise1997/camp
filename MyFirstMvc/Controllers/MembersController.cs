using MyFirstMvc.Models.EFModels;
using MyFirstMvc.Models.Infra;
using MyFirstMvc.Models.ViewModels.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMvc.Controllers
{
	public class MembersController : Controller
	{
		// GET: Members
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterVm vm)
		{
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();

				ViewBag.Errors = errors;
				return View(vm);
			}
			try
			{
				RegisterMember(vm);
			}
			catch (Exception ex)
			{
				ViewBag.Errors = new List<string> { ex.Message }; //添加異常訊息
				return View(vm);
			}

			return View("RegisterConfirm");


		}

		public ActionResult ActiveRegister(int memberId, string confirmCode)
		{
			if (memberId <= 0 || string.IsNullOrEmpty(confirmCode))
			{
				return View();
			}

			var db = new AppDbContext();


			//根據memberId, confirmCode 取得Member
			var member = db.Members.FirstOrDefault(p => p.Id == memberId && p.ConfirmCode == confirmCode && p.IsConfirmed == false);
			if (member == null)
			{
				return View();
			}

			member.IsConfirmed = true;
			member.ConfirmCode = null;
			db.SaveChanges();

			return View();
		}

		private void RegisterMember(RegisterVm vm)
		{
			var db = new AppDbContext();
			var errors = new List<string>();


			var memberInDb = db.Members.FirstOrDefault(p => p.Account == vm.Account);
			if (memberInDb != null)
			{
				errors.Add("帳號已存在");
			}

			if (!isAccountValid(vm.Account))
			{
				errors.Add("帳號不能是中文");
			}

			if (!IsPasswordValid(vm.Password))
			{
				errors.Add("密碼不符合要求");
			}

			if (errors.Count > 0)
			{
				throw new Exception(string.Join("; ", errors));
			}

			var member = vm.ToEFModel();


			db.Members.Add(member);
			db.SaveChanges();

			// todo發送驗證信
			var confirmationUrl = GenerateConfirmationLink(member.Id, member.ConfirmCode);
			var emailHelper = new EmailHelper();
			emailHelper.SendConfirmRegisterEmail(confirmationUrl, member.Name, member.Email);

		}

		//寄送信件
		private string GenerateConfirmationLink(int memberId, string confirmCode)
		{
			var confirmRegisterUrl = Url.Action("ActiveRegister", "Members", new { memberId, confirmCode }, protocol: Request.Url.Scheme);
			return $"{confirmRegisterUrl}";
		}

		private bool IsPasswordValid(string password)
		{
			string pattern = @"^(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
			return Regex.IsMatch(password, pattern);
		}

		private bool isAccountValid(string account)
		{
			string pattern = @"^[^\u4e00-\u9fa5]+$";
			return Regex.IsMatch(account, pattern);
		}

	}
}
