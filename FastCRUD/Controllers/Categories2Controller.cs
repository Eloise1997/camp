using FastCRUD.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastCRUD.Controllers
{
    public class Categories2Controller : Controller
    {
        // GET: Categories2
        public ActionResult Index()
		{
			List<Category> categories = GetCategories();
			return View(categories);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Category model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			// create record
			try
			{
				CreateCategory(model);
			}catch(ArgumentException ex)
			{
				ModelState.AddModelError("DisplayOrder", ex.Message);
				return View(model);
			}
			catch (Exception ex)
			{
				// 將錯誤放入 ModelState
				// 但由於不知道算是哪一個欄位錯,所以就不指定欄位名稱
				ModelState.AddModelError("",ex.Message);
				return View(model);
			}
			
			return RedirectToAction("Index"); // 轉向到 index action

		}
		[HttpGet]
		public ActionResult Edit(int id) 
		{
			var category = new AppDbContext()
							.Categories
							.Where(c => c.Id == id)
							.FirstOrDefault();

			if(category == null)return HttpNotFound();

			return View(category);
		}
		[HttpPost]
		public ActionResult Edit(Category model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			// update record
			try
			{
				UpdateCategory(model);

				return View(model);
			}

			catch (Exception ex)
			{
				// 將錯誤放入 ModelState
				// 但由於不知道算是哪一個欄位錯,所以就不指定欄位名稱
				ModelState.AddModelError("", ex.Message);
				return View(model);
			}

			return RedirectToAction("Index"); // 轉向到 index action
		}
		public ActionResult Delete(int id)
		{
			var dbContext = new AppDbContext();
			var category = dbContext.Categories.Find(id);

			if(category == null) return HttpNotFound();

			dbContext.Categories.Remove(category);
			dbContext.SaveChanges();

			return RedirectToAction("Index");

		}
		private void UpdateCategory(Category model)
		{
			var dbContext = new AppDbContext();

			var categoryInDb = dbContext.Categories.Find(model.Id);
			if (categoryInDb == null) throw new ArgumentException("找不到要修改的資料");

			categoryInDb.Name= model.Name;
			categoryInDb.DisplayOrder = model.DisplayOrder;
			dbContext.SaveChanges();

		}

		private static void CreateCategory(Category model)
		{
			if(model.DisplayOrder < 0)
			{
				throw new ArgumentException("DisplayOrder 必須大於等於 0 ");
			}
			//throw new ArgumentException("下午茶時間,不想新增...");

			using (AppDbContext db = new AppDbContext())
			{
				db.Categories.Add(model);
				db.SaveChanges();
			}
		}

		private  List<Category> GetCategories()
		{
			return new AppDbContext()
							.Categories
							.OrderBy(c => c.DisplayOrder)
							.ToList();
		}
	}
}