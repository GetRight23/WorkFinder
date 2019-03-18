using DatabaseDao;
using Models;
using NLog;
using System;
using System.Collections.Generic;

namespace DatabaseFiller
{
	class OrdersListFiller : DBFiller
	{
		public OrdersListFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			try
			{
				List<Worker> workers = Storage.WorkerDao.selectEntities();

				List<OrdersList> ordersLists = new List<OrdersList>();
				for (int i = 0; i < workers.Count; i++)
				{
					int orderListExists = Random.Next(0, 2);
					if(orderListExists == 1)
					{
						OrdersList ordersList = new OrdersList()
						{
							IdWorker = workers[i].Id
						};
						ordersLists.Add(ordersList);
					}
				}
				Storage.OrderListDao.insertEntities(ordersLists);
				Logger.Info("Orders list table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				Logger.Error("Orders list filler filling failed");
			}
		}
	}
}
