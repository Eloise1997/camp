using MyFirstApi.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyFirstApi.Controllers.Categories
{
    public class CategoriesApiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var categories = new AppDbContext().Categories
                .OrderBy(c => c.Name)
                .Select(c => new { Id = c.Id, Name = c.Name })
                .ToList();
            return Ok(categories); // 傳回json內容多筆id,name
        }
    }
}
