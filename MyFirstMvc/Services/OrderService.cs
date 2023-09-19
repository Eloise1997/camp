using MyFirstMvc.Models.EFModels;
using MyFirstMvc.Models.Repositories;
using MyFirstMvc.Models.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstMvc.Services
{
    public class OrderService
    {
        public void CreateOrder(string account, CartVm cart, CheckoutVm vm)
        {
            // 拉到Repo
            var db = new AppDbContext();
            var memberId = db.Members.First(m => m.Account == account).Id;

            var order = new Order
            {
                MemberId = memberId,
                // Name = vm.Name, 缺少~~~~~~
                // Phone = vm.Phone, 缺少~~~~~~
                OrderTime = DateTime.Now,
                Status = 1, // 建立enum
                PaymentTypeId = Convert.ToInt32(vm.PaymnetType),
                TotalPrice = cart.TotalPrice,
            };
            // 新增訂單明細
            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    RoomId = item.RoomId,
                    Days = item.Days,
                    CheckInDate = Convert.ToDateTime(item.CheckInDate),
                    CheckOutDate = Convert.ToDateTime(item.CheckOutDate),
                    ExtraBed = item.ExtraBed,
                    ExtraBedPrice = item.ExtraBedPrice,
                    SubTotal = item.SubTotal
                };
                order.OrderItems.Add(orderItem);
            }
            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}