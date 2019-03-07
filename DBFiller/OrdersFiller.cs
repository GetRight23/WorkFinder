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
			FileLoader loader = new FileLoader();
			List<string> orderTables = new List<string>();
			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\orders.txt");

			orderTables.AddRange(loader.entities);

			List<Orderslist> orderslists = Storage.OrderListDao.selectEntities();
			List<OrderTable> orders = new List<OrderTable>();

			Random rand = new Random();

			for (int i = 0; i < orderTables.Count; i++)
			{
				OrderTable order = new OrderTable()
				{
					Info = "Test info",
					IdOrderList = orderslists[rand.Next(0, orderslists.Count)].Id
				};
				orders.Add(order);
			}
			Storage.OrderTableDao.insertEntities(orders);
		}
	}
}
