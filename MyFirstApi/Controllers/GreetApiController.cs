using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace MyFirstApi.Controllers
{
    public class GreetApiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Greet(string name)
        {
            return Ok("Hello" + name);
        }

		[Route("Greet2")]
		[HttpPost]
		public IHttpActionResult greet2(Greet model)
		{
			var data = new { Age = model.Age * 2, Name = model.Name };
			return Ok(data);
		}

		[Route("Greet3")]
		[HttpPost]
		public IHttpActionResult Greet3(Greet model, int num)
		{
			var data = new { Age = model.Age * 3, Name = model.Name };
			return Ok(data);
		}
	}

	public class Greet
	{
        public int Age { get; set; }
        public string Name { get; set; }

    }
}
