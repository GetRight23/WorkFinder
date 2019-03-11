using DatabaseDao;
using Models;
using NLog;
using System;
using System.Collections.Generic;

namespace DBFiller
{
	class OrdersFiller : DBFiller
	{
		public OrdersFiller(Storage storage) : base(storage) {}
		public override void fillEntities()
		{
			try
			{
				List<OrdersList> ordersLists = Storage.OrderListDao.selectEntities();

				int counter = 0;
				List<Order> orders = new List<Order>();
				for (int i = 0; i < ordersLists.Count; i++)
				{
					int ordersCount = Random.Next(0, 2);
					for (int j = 0; j < ordersCount; j++)
					{
						Order order = new Order()
						{
							Info = $"Test order info {counter}",
							IdOrderList = ordersLists[i].Id
						};
						orders.Add(order);
					}
				}
				Storage.OrderDao.insertEntities(orders);
				Logger.Info("Orders table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}		
		}
	}
}
