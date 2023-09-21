using MyFirstMvc.Models.EFModels;
using FProjectCamping.Models.ViewModels;
using MyFirstMvc.Models.Infra;
using MyFirstMvc.Models.Repositories;
using MyFirstMvc.Models.ViewModels.Members;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;

namespace FProjectCamping.Members.Controllers
{
    public class MembersController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
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

        public ActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Login(LoginVm vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			try
			{
				ValidLogin(vm);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}

			var processResult = ProcessLogin(vm);

			Response.Cookies.Add(processResult.Cookie);
			return Redirect(processResult.ReturnUrl);

		}

		public ActionResult Logout()
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Home/Index/");

		}

        [Authorize]
        public ActionResult EditProfile()
        {
            var currentUserAccount = User.Identity.Name;
            var vm = GetMemberProfile(currentUserAccount);

            return View(vm);
        }

		[Authorize]
        [HttpPost]
		public ActionResult EditProfile(EditProfileVm vm)
		{
            var currentUserAccount = User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                UpdateProfile(vm, currentUserAccount);
            }catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction("Index");

        }

		[Authorize]
		public ActionResult MyOrders() // todo
		{
			var orders = new OrderRepository().GetOrders("aliee");
			return View(orders);
		}

		private void UpdateProfile(EditProfileVm vm, string account)
        {
            var db = new AppDbContext();
            var memberInDb=db.Members.FirstOrDefault(p=>p.Id==vm.Id);

            if (memberInDb.Account != account)
            {
                throw new Exception("您沒有權限修改別人資料");

            }

            memberInDb.Account = memberInDb.Account;
            memberInDb.Name = vm.Name;
            memberInDb.Email= vm.Email;
            memberInDb.PhoneNum = vm.PhoneNum;
            memberInDb.Birthday = memberInDb.Birthday;

            db.SaveChanges();
            
        }

        private EditProfileVm GetMemberProfile(string currentUserAccount)
		{
            var db = new AppDbContext();

            var member = db.Members.FirstOrDefault(p => p.Account == currentUserAccount);
            if(member == null)
            {
                throw new Exception("帳號不存在");
            }
           

            var vm =member.ToEditProfileVm();
            return vm;
		}


		private void ValidLogin(LoginVm vm)
		{
			var db= new AppDbContext();
            var member = db.Members.FirstOrDefault(p=>p.Account == vm.Account);

            if (member == null)
            {
                throw new Exception("帳號或密碼有誤");
            }

            if (member.IsConfirmed == false)
            {
                throw new Exception("您尚未開通會員資格，請先收確認信，並點選信里的連結，完成驗證，才能登入本網站");
            }
            if (member.Enabled == true)
            {
                throw new Exception("您的帳號已被停權");
            }

            var salt = HashUtility.GetSalt();
            var hashPassword = HashUtility.ToSHA256(vm.Password,salt);


            if (string.Compare(member.Password, hashPassword,true)!=0){
                throw new Exception("帳號或密碼有誤");
            }

		}
		private (string ReturnUrl,HttpCookie Cookie) ProcessLogin(LoginVm vm)
		{
            var rememberMe = vm.RememberMe;
            var account =vm.Account;
            var roles = string.Empty;
			DateTime expirationDate;

            //紀錄登入時間
			if (rememberMe)
			{
				expirationDate = DateTime.Now.AddDays(2);
			}
			else
			{
				expirationDate = DateTime.Now.AddHours(1); 
			}

			var ticket =
                new FormsAuthenticationTicket(
                    1,
                    account,
                    DateTime.Now,
					expirationDate,
                    rememberMe,
                    roles,
                    "/"
                 );
            
            //將他加密
            var value =FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

            var url = FormsAuthentication.GetRedirectUrl(account, true);

            return (url,cookie);

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
            
            if (!IsAccountValid(vm.Account))
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

            // 發送驗證信
            var confirmationUrl = GenerateConfirmationLink(member.Id, member.ConfirmCode);

            // 使用 EmailHelper 來發送確認郵件
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

        private bool IsAccountValid(string account)
        {
            string pattern = @"^[^\u4e00-\u9fa5]+$";
            return Regex.IsMatch(account, pattern);
        }

    }
}