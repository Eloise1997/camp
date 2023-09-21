﻿using MyFirstMvc.Models.ViewModels;
using MyFirstMvc.Models.ViewModels.Cart;
using MyFirstMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMvc.Controllers
{
    public class CartController : Controller
    {
        /// <summary>
        /// 將 productId加入購物車
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        // GET: Cart
        [Authorize]
        public string AddItem(CartItemsVm vm)
        {
            string buyer = User.Identity.Name; // 買家帳號
            var result = new CartService().AddToCart(buyer, vm); //加入購物車

            return result; //沒傳回任何東西
        }

        [Authorize]
        public ActionResult Checkout()
        {
            var buyer = User.Identity.Name; 
            var cart = new CartService().GetOrCreateCart(buyer);

            if (cart.AllowCheckout == false) ViewBag.ErrorMessage = "購物車是空的,無法進行結帳";

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Checkout(CheckoutVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var cartService = new CartService();

            var buyer = User.Identity.Name;
            var cart = cartService.GetOrCreateCart(buyer);

            if (cart.AllowCheckout == false)
            {
                ModelState.AddModelError("", "購物車是空的,無法進行結帳");
                return View(vm);
            }

            cartService.ProcessCheckout(buyer, cart, vm);
            return View("ConfirmCheckout"); // todo 轉導到結帳頁
        }

        public ActionResult OrderInfo()
        {
            return View();
        }

		// GET: Cart
		public ActionResult Cart()
        {
			// memberId 也許是從登入資訊來
			var model = new CartService().Get("aliee");
			
            {

                var model2 = new CartVm()
                {
                    Items = new List<CartItemsVm>
                {
                    new CartItemsVm
                    {
                        Id = 1,
                        RoomType = "森林雙人房",
                        RoomNumber = "201",
                        CheckInDate = "2023-09-10",
                        CheckOutDate = "2023-09-10",
                        Days = 1,
                        ExtraBed = false,
                        ExtraBedPrice = 0,
                        RoomPrice = 1000,
						//SubTotal = 1000,
					},
                    new CartItemsVm
                    {
                        Id = 2,
                        RoomType = "河畔雙人房",
                        RoomNumber = "104",
                        CheckInDate = "2023-09-11",
                        CheckOutDate = "2023-09-12",
                        Days = 1,
                        ExtraBed = true,
                        ExtraBedPrice = 500,
                        RoomPrice = 1000,
						//SubTotal = 1500,
					}
                }
                };

			return View(model2);
				//Total = 2500,
				//ExtraBedPrice = 500,
			}
            return View(model);
        }

        public ActionResult DeleteCartItem(int cartItemId)
        {
            new CartService().DeleteCartItem(cartItemId);
            return new EmptyResult();
        }
    }
}