using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFiller
{
	class OrdersFiller : DBFiller
	{
		public OrdersFiller(Storage storage) : base(storage) {}
		public override void fillEntities()
		{
			List<string> orderTables = new List<string>();

			List<Orderslist> orderslists = Storage.OrderListDao.selectEntities();
			List<OrderTable> orders = new List<OrderTable>();

			fileLoader.load(@".\res\orders.txt");
			orderTables.AddRange(fileLoader.Entities);
			
			for (int i = 0; i < orderTables.Count; i++)
			{
				OrderTable order = new OrderTable()
				{
					Info = "Test info",
					IdOrderList = orderslists[Random.Next(0, orderslists.Count)].Id
				};
				orders.Add(order);
			}
			Storage.OrderTableDao.insertEntities(orders);
		}
	}
}
