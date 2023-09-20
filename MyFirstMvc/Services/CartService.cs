using MyFirstMvc.Models;
using MyFirstMvc.Models.EFModels;
using MyFirstMvc.Models.Repositories;
using MyFirstMvc.Models.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MyFirstMvc.MvcApplication;

namespace MyFirstMvc.Services
{
    public class CartService
    {
        public CartVm Get(string account)
        {
            var vm = new CartVm();
            var cartRepo = new CartRepository();
            var cartItemRepo = new CartItemsRepository();

            var cart = cartRepo.GetByMember(account);
            var cartItem = cartItemRepo.Get(cart.Id);

            vm = AutoMapperHelper.MapperObj.Map<CartVm>(cart);
            vm.Items = AutoMapperHelper.MapperObj.Map<List<CartItemsVm>>(cartItem);

            return vm;
        }

        public string AddToCart(string buyer, CartItemsVm vm)
        {
            string msg = "不可新增重複的項目";

            // 取得目前購物車主檔,若沒有就立即新增一筆並傳回
            CartVm cart = GetOrCreateCart(buyer);

            // 檢查是否有加入重複的明細檔，有的話，阻擋加入
            if (cart != null && !this.IsRepeat(cart, vm))
            {
                var entity = AutoMapperHelper.MapperObj.Map<CartItem>(vm);
                entity.CartId = cart.Id;

                //加入購物車,若明細不存在就新增一筆,若存在就更新數量
                new CartItemsRepository().AddCartItem(entity);

                msg = "加入購物車~";
            }

            return msg;
        }

        /// <summary>
        /// 取得目前購物車主檔,若沒有就立即新增一筆並傳回
        /// </summary>
        /// <param name="buyer"></param>
        /// <returns></returns>
        public CartVm GetOrCreateCart(string buyer)
        {
            var cartRepo = new CartRepository();
            var cart = cartRepo.GetByMember(buyer);

            // 沒有購物車紀錄，立即新增一筆並傳回
            if (cart == null)
            {
                cart = new Cart { MemberAccount = buyer };
                var id = cartRepo.AddNew(cart);

                return new CartVm
                {
                    Id = id,
                    MemberAccount = cart.MemberAccount,
                    Items = new List<CartItemsVm>()
                };
            }

            // 傳回目前購物車主檔/明細檔紀錄

            return new CartVm
            {
                Id = cart.Id,
                MemberAccount = cart.MemberAccount,
                Items = AutoMapperHelper.MapperObj.Map<List<CartItemsVm>>(cart.CartItems),
            };
        }

        public void DeleteCartItem(int cartItemId)
        {
            new CartItemsRepository().Delete(cartItemId);
        }

        public void ProcessCheckout(string account, CartVm cart, CheckoutVm vm)
        {
            // 建立訂單主檔明細檔
            new OrderService().CreateOrder(account, cart, vm);

            // 清空購物車
            new CartRepository().EmptyCart(account);
        }

        /// <summary>
        /// 比較欲新增的明細檔是否重複
        /// </summary>
        /// <param name="cartVm"></param>
        /// <param name="vm"></param>
        /// <returns></returns>
        private bool IsRepeat(CartVm cartVm, CartItemsVm vm)
        {
            return cartVm.Items.Any(item =>
                item.RoomId == vm.RoomId &&
                Convert.ToDateTime(item.CheckInDate).Date == Convert.ToDateTime(vm.CheckInDate).Date &&
                Convert.ToDateTime(item.CheckOutDate).Date == Convert.ToDateTime(vm.CheckOutDate).Date);
        }
    }
}