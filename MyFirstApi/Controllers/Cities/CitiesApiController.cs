using MyFirstApi.Models.Repositories;
using MyFirstApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyFirstApi.Controllers.Cities
{
    public class CitiesApiController : ApiController
    {
		[HttpGet]
		public IHttpActionResult Get()
		{
			var cities = new CityRepository().GetAll();
			var vm = cities.Select(city => city.ToViewModel()).ToList();

			return Ok(vm);
		}
	}
}
