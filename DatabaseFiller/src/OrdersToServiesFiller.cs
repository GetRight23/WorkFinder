using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;

namespace DatabaseFiller
{
	class OrdersToServiesFiller : DBFiller
	{
		public OrdersToServiesFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			try
			{
				List<Order> orders = Storage.OrderDao.selectEntities();
				List<Service> services = Storage.ServiceDao.selectEntities();

				List<OrderToService> ordersToServices = new List<OrderToService>();
				for (int i = 0; i < orders.Count; i++)
				{
					int servicesPerOrder = Random.Next(1, services.Count);
					for (int j = 0; j < servicesPerOrder; j++)
					{
						OrderToService orderToService = new OrderToService()
						{
							IdOrder = orders[i].Id,
							IdService = services[j].Id
						};
						ordersToServices.Add(orderToService);
					}
					ordersToServices.Clear();
				}
				Logger.Info("Order to service table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}
		}
	}
}
