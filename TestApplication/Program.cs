using System;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using DatabaseDao;
using System.Collections.Generic;

namespace TestApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;

			ApplicationContext ctx = new ApplicationContext();

			DatabaseDao<Address> addressDao = new DatabaseDao<Address>(ctx, ctx.Address);
			DatabaseDao<City> cityDao = new DatabaseDao<City>(ctx, ctx.City);
			DatabaseDao<CityDistricts> cityDistrictsDao = new DatabaseDao<CityDistricts>(ctx, ctx.CityDistricts);
			DatabaseDao<Feedback> feedbackDao = new DatabaseDao<Feedback>(ctx, ctx.Feedback);
			DatabaseDao<Orderslist> orderListDao = new DatabaseDao<Orderslist>(ctx, ctx.Orderslist);
			DatabaseDao<OrderTable> orderTableDao = new DatabaseDao<OrderTable>(ctx, ctx.OrderTable);
			DatabaseDao<ProfCategory> profCategoryDao = new DatabaseDao<ProfCategory>(ctx, ctx.ProfCategory);
			DatabaseDao<Profession> profesionDao = new DatabaseDao<Profession>(ctx, ctx.Profession);
			DatabaseDao<Service> serviceDao = new DatabaseDao<Service>(ctx, ctx.Service);
			DatabaseDao<Worker> workerDao = new DatabaseDao<Worker>(ctx, ctx.Worker);

			Console.ReadKey();
			ctx.Database.CloseConnection();
		}
	}
}
