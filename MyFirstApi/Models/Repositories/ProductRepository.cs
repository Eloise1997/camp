using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstApi.Models.Repositories
{
	public class ProductRepository
	{
		private static List<Product> _products;
		static ProductRepository()
		{
			_products = Enumerable.Range(1, 10).Select(i => new Product
			{
				Id = i,
				Name = $"Product {i}",
				UnitPrice = i * 1000
			}).ToList();
		}

		public List<Product> GetAll()=> _products;

		public Product Get(int id)
		{
			return _products.FirstOrDefault(p => p.Id == id);
		}
	}
}