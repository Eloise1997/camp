using Microsoft.Ajax.Utilities;
using MyFirstMvc.Models.EFModels;
using MyFirstMvc.Models.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstMvc.Models.Repositories
{
    public class CartRepository : BaseRepository<Cart>
    {
        private AppDbContext _db = new AppDbContext();

        /// <summary>
        /// 透過會員Key取得購物車資訊
        /// </summary>
        /// <param name="memberId">會員key</param>
        /// <returns></returns>
        public Cart GetByMember(string account)
        {
            return _db.Carts.FirstOrDefault(x => x.MemberAccount == account);
        }

        public int AddNew(Cart cart)
        {
            _db.Carts.Add(cart);
            _db.SaveChanges();
            return cart.Id;
        }
    }
}