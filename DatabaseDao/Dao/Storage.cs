using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DatabaseDao
{
    public class Storage
    {
        private static ApplicationContext ctx = new ApplicationContext();
        private static Storage instance = null;

        private Storage()
        {
			AddressDao = new DatabaseDao<Address>(ctx, ctx.Address);
            CityDao = new DatabaseDao<City>(ctx, ctx.City);
            CityDistrictsDao = new DatabaseDao<CityDistricts>(ctx, ctx.CityDistricts);
            FeedbackDao = new DatabaseDao<Feedback>(ctx, ctx.Feedback);
            OrderListDao = new DatabaseDao<Orderslist>(ctx, ctx.Orderslist);
            OrderTableDao = new DatabaseDao<OrderTable>(ctx, ctx.OrderTable);
            ProfCategoryDao = new DatabaseDao<ProfCategory>(ctx, ctx.ProfCategory);
            ProfesionDao = new DatabaseDao<Profession>(ctx, ctx.Profession);
            ServiceDao = new DatabaseDao<Service>(ctx, ctx.Service);
            WorkerDao = new DatabaseDao<Worker>(ctx, ctx.Worker);
            OrderToServiceDao = new OrderToServiceDao(ctx, ctx.OrderToService);
        }

        public static Storage getInstance()
        {
            if (instance == null)
                instance = new Storage();
            return instance;
        }

        public DatabaseDao<Address> AddressDao { get; private set; }
        public DatabaseDao<City> CityDao { get; private set; }
        public DatabaseDao<CityDistricts> CityDistrictsDao { get; private set; }
        public DatabaseDao<Feedback> FeedbackDao { get; private set; }
        public DatabaseDao<Orderslist> OrderListDao { get; private set; }
        public DatabaseDao<OrderTable> OrderTableDao { get; private set; }
        public DatabaseDao<ProfCategory> ProfCategoryDao { get; private set; }
        public DatabaseDao<Profession> ProfesionDao { get; private set; }
        public DatabaseDao<Service> ServiceDao { get; private set; }
        public DatabaseDao<Worker> WorkerDao { get; private set; }
        public OrderToServiceDao OrderToServiceDao { get; set; }
    }
}
