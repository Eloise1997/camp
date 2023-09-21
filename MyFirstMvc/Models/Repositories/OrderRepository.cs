using MyFirstMvc.Models.ViewModels.Members;
using MyFirstMvc.Models.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstMvc.Models.Repositories
{
	public class OrderRepository
	{
		private static List<MyOrder> _orders = new List<MyOrder>();

		static OrderRepository()
		{
			_orders = new List<MyOrder>
			{
				new MyOrder
				{
					Id = 1,
					OrderNumber = "669511",
					OrderTime = new System.DateTime(2023, 9, 10),
					OrderStatus = "新訂單",
					PaymentType = "信用卡刷卡成功",
					Total = 1000,
					OrderItems = new List<OrderItems>
					{
						new OrderItems
						{
							RoomType = "森林四人房",
							CheckInDate = "2023/09/15",
							CheckOutDate = "2023/09/17",
							Days = 2,
							SubTotal = 3500
						}
						
					}
				
				},

				new MyOrder
				{
					Id = 2,
					OrderNumber = "664566",
					OrderTime = new System.DateTime(2023, 9, 10),
					OrderStatus = "已宅配送達",
					PaymentType = "已 ATM 匯款",
					Total = 1200,
					OrderItems = new List<OrderItems>
					{
						new OrderItems
						{
							RoomType = "森林四人房",
							CheckInDate = "2023/09/15",
							CheckOutDate = "2023/09/17",
							Days = 2,
							SubTotal = 3500
						}

					}
				},

				new MyOrder
				{
					Id = 3,
					OrderNumber = "638100",
					OrderTime = new System.DateTime(2023, 7, 13),
					OrderStatus = "已送至便利商店 7-11(已取貨)",
					PaymentType = "7-ELEVEN 取貨付款",
					Total = 600,
					OrderItems = new List<OrderItems>
					{
						new OrderItems
						{
							RoomType = "森林四人房",
							CheckInDate = "2023/09/15",
							CheckOutDate = "2023/09/17",
							Days = 2,
							SubTotal = 3500
						}

					}
				},

				new MyOrder
				{
					Id = 4,
					OrderNumber = "638090",
					OrderTime = new System.DateTime(2023, 6, 13),
					OrderStatus = "取消",
					PaymentType = "7-ELEVEN 取貨付款",
					Total = 600,
					OrderItems = new List<OrderItems>
					{
						new OrderItems
						{
							RoomType = "森林四人房",
							CheckInDate = "2023/09/15",
							CheckOutDate = "2023/09/17",
							Days = 2,
							SubTotal = 3500
						}

					}
				},
				new MyOrder
				{
					Id = 5,
					OrderNumber = "638087",
					OrderTime = new System.DateTime(2023, 6, 13),
					OrderStatus = "取消",
					PaymentType = "刷卡未完成",
					Total = 600,
					OrderItems = new List<OrderItems>
					{
						new OrderItems
						{
							RoomType = "森林四人房",
							CheckInDate = "2023/09/15",
							CheckOutDate = "2023/09/17",
							Days = 2,
							SubTotal = 3500
						}

					}
				},

			};
		}

		public List<MyOrder> GetOrders(string account)
		{
			return _orders.OrderByDescending(x => x.OrderTime).ToList();
		}
	}
}
