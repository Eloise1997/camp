﻿using MyFirstMvc.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyFirstMvc.Models.Repositories
{
    public class CartItemsRepository : BaseRepository<CartItem>
    {
        private AppDbContext _db = new AppDbContext();

        /// <summary>
        /// 取得購物車明細
        /// </summary>
        /// <param name="cartId">購物車key</param>
        /// <returns></returns>
        public List<CartItem> Get(int cartId)
        {
            return _db.CartItems.Where(x => x.CartId == cartId).ToList();
        }

        /// <summary>
        /// 加入購物車,若明細不存在就新增一筆
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId"></param>
        /// <param name="qty"></param>
        public void AddCartItem(CartItem cartItem)
        {
            var db = new AppDbContext();

            // 不能新增相同資料
            var entity = Get(cartItem.CartId, cartItem.RoomId, cartItem.CheckInDate, cartItem.CheckOutDate);
            if (entity == null)
            {
                db.CartItems.Add(cartItem);
                db.SaveChanges();
                return;
            }
        }

        /// <summary>
        /// 取得房號、入住日、退房日 相同的明細
        /// </summary>
        /// <param name="roomId">房號</param>
        /// <param name="checkInDate">入住日</param>
        /// <param name="checkOutDate">退房日</param>
        /// <returns></returns>
        public CartItem Get(int cartId, int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            return _db.CartItems.FirstOrDefault(x => x.CartId == cartId &&
                    x.RoomId == roomId &&
                    DbFunctions.TruncateTime(x.CheckInDate) == DbFunctions.TruncateTime(checkInDate) &&
                    DbFunctions.TruncateTime(x.CheckOutDate) == DbFunctions.TruncateTime(checkOutDate));
        }
    }
}