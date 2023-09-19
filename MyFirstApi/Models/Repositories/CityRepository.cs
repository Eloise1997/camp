using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstApi.Models.Repositories
{
	public class CityRepository
	{
		private static List<City> _cities = new List<City>();

		static CityRepository() //static靜態的建構函數只會run一次
		{
			int counter = 1;

			_cities = Enumerable.Range(1, 10).Select(x => new City
			{
				Id = x,
				Name = $"City {x}",
				Districts = Enumerable.Range(1, 9).Select(y => new District
				{
					Id = counter,
					Name = $"District {counter++}"
				}).ToList()
			}).ToList();
		}

		public List<City> GetAll()
		{
			return _cities;
		}
	}
}