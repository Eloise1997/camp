using MyFirstApi.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstApi.Controllers
{
    public class Products2Controller : Controller
    {
        // GET: Products2
        public ActionResult Index2()
        {
            var products = new ProductRepository().GetAll();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            var product = new ProductRepository().Get(id);
            return View(product);
        }
    }
}