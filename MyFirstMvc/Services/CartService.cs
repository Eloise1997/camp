using MyFirstMvc.Models;
using MyFirstMvc.Models.EFModels;
using MyFirstMvc.Models.Repositories;
using MyFirstMvc.Models.ViewModels;
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
            CartVm cart = GetOrCreateCart(buyer, vm);

            if (cart != null)
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
        private CartVm GetOrCreateCart(string buyer, CartItemsVm vm)
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
            // 1.先檢查是否有加入重複的明細檔，有的話，不可加入，並回傳Null

            if (!cart.CartItems.Any(item =>
                    item.RoomId == vm.RoomId &&
                    item.CheckInDate.Date == Convert.ToDateTime(vm.CheckInDate).Date &&
                    item.CheckOutDate.Date == Convert.ToDateTime(vm.CheckOutDate).Date))
                return new CartVm
                {
                    Id = cart.Id,
                    MemberAccount = cart.MemberAccount,
                    Items = AutoMapperHelper.MapperObj.Map<List<CartItemsVm>>(cart.CartItems),
                };
            else
                return new CartVm();
        }
    }
}