using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstApi.Models
{
		public class Product
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public int UnitPrice { get; set; }
		}
		public class Cart
		{
			public List<CartItem> Items { get; set; } = new List<CartItem>();
			public int Total => Items.Sum(i => i.SubTotal);
		}
		public class CartItem
		{
			public int Id { get; set; }
			public Product Product { get; set; }
			public int Qty { get; set; }
			public int SubTotal => Product.UnitPrice * Qty;
		}
}
