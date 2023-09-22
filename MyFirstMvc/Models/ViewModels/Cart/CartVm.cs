using System.Collections.Generic;
using System.Linq;

namespace MyFirstMvc.Models.ViewModels.Cart
{
	public class CartVm
	{
		public int Id { get; set; }

		public string MemberAccount { get; set; }

		public List<CartItemsVm> Items { get; set; }

		//public int TotalPrice => Items.Sum(x => x.SubTotal);

		public int TotalPrice { get; set; }

		public bool AllowCheckout => Items.Any(); // 至少要有一筆明細資料才可以結帳
	}
}