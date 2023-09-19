using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstApi.Models.Repositories
{
	public class CartRepository
	{
		private static Cart _cart = new Cart();

		/// <summary>
		/// 增減購買商品數量,若商品不存在則新增, 若數量<=0則表示數量要減少,當減少後數量小於零,則移除該CartItem
		/// </summary>
		/// <param name="product"></param>
		/// <param name="quantity"></param>
		public Cart TryAddToCart(Product product, int quantity = 1)
		{
			var item = _cart.Items.FirstOrDefault(i => i.Product.Id == product.Id);
			if (item == null)
			{
				quantity = quantity <= 0 ? 1 : quantity; // 若傳入數量<=0,則預設為1
				_cart.Items.Add(new CartItem
				{
					Id = _cart.Items.Count == 0 ? 1 : _cart.Items.Max(i => i.Id) + 1,
					Product = product,
					Qty = quantity
				});
			}
			else
			{
				item.Qty += quantity;
				// 如果更新後數量<=0,則移除
				if (item.Qty <= 0)
				{
					RemoveFromCart(item.Id);
				}
			}
			return _cart;
		}
		public void RemoveFromCart(int id)
		{
			var item = _cart.Items.FirstOrDefault(i => i.Id == id);
			if (item != null)
			{
				_cart.Items.Remove(item);
			}
		}
		public Cart GetCart()
		{
			return _cart;
		}
	}
}