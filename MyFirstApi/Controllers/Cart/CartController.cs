using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstApi.Controllers.Cart
{
	public class CartController : Controller
	{
		// GET: Cart
		public ActionResult CheckOut()
		{
			return View();
		}

		public ActionResult Index()
		{
			return View();
		}
	}
}