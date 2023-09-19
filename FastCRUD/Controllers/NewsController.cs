using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FastCRUD.Models.EFModels;
using FastCRUD.Models.ViewModels;

using static FastCRUD.AutoMapperHelper;

namespace FastCRUD.Controllers
{
    public class NewsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: News
        public ActionResult Index()
		{
			List<NewsIndexVm> vms = GetNews();

			return View(vms);
		}

		private List<NewsIndexVm> GetNews()
		{
			//return db.News.Select(n => new NewsIndexVm
			//{
			//	Id = n.Id,
			//	Title = n.Title,
			//	Description = n.Description,
			//	CreatedTime = n.CreatedTime,
			//	ModifiedTime = n.ModifiedTime
			//}).ToList();

			//// 取得News方法1
			//var news = db.News.ToList();
			//// 轉換成 NewsIndexVm
			//var vms = AutoMapperHelper.MapperObj.Map<List<News>, List<NewsIndexVm>>(news);

			//return vms;

			// 取得News方法2
            return MapperObj.Map<List<News>, List<NewsIndexVm>>(db.News.ToList());

		}

		// GET: News/Details/5
		//public ActionResult Details(int? id)
		//{
		//    if (id == null)
		//    {
		//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//    }
		//    News news = db.News.Find(id);
		//    if (news == null)
		//    {
		//        return HttpNotFound();
		//    }
		//    return View(news);
		//}

		// GET: News/Create
		public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsVm vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CreateNews(vm);
                } catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            if(ModelState.IsValid) {
				return RedirectToAction("Index");
			}
			

			return View(vm);
        }

		private void CreateNews(NewsVm vm)
		{
            //var news = new News
            //{
            //	Title = vm.Title,
            //	Description = vm.Description,
            //	CreatedTime = DateTime.Now,
            //	ModifiedTime = DateTime.Now,
            //};

            var news = MapperObj.Map<NewsVm, News>(vm);

			db.News.Add(news);
			db.SaveChanges();
		}

		// GET: News/Edit/5
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            //var vm = new NewsVm
            //{
            //    Id = news.Id,
            //    Title = news.Title,
            //    Description = news.Description,
            //};

            var vm = MapperObj.Map<News, NewsVm>(news);
            return View(vm);
        }

        // POST: News/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsVm vm)
        {
            var newInDb = db.News.Find(vm.Id);
            newInDb.Title = vm.Title;
            newInDb.Description = vm.Description;
            newInDb.ModifiedTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                //db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
