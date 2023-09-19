using MyFirstApi.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyFirstApi.Controllers.Cart
{
    public class CartApiController : ApiController
    {
		public class RequestInfo
		{
			public int ProductId { get; set; }
			public int Quantity { get; set; } = 1;
		}
		// POST api/<controller>
		[HttpPost]
		public IHttpActionResult AddToCart(RequestInfo model)
		{
			var productId = model.ProductId;
			var quantity = model.Quantity;
			var product = new ProductRepository().Get(productId);
			if (product == null)
			{
				return NotFound();
			}
			var cart = new CartRepository().TryAddToCart(product, quantity);
			return Ok(cart);
		}
		// GET api/<controller>
		[HttpGet]
		public IHttpActionResult GetCart()
		{
			var cart = new CartRepository().GetCart();
			return Ok(cart);
		}
	}
}
