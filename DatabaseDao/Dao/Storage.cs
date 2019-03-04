using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Models;

namespace DatabaseDao
{
    public abstract class Storage
    {
        private static ApplicationContext ctx = new ApplicationContext();
        public Storage(DbConnection connection)
        {
            connection.Open();
            createCityTable(connection);
            createCityDistrictsTable(connection);
            createAddressTable(connection);
            createWorkerTable(connection);
            createFeedbackTable(connection);
            createOrderListTable(connection);
            createOrderTable(connection);
            createProfCategoryTable(connection);
            createProfessionTable(connection);
            createServiceTable(connection);
            createOrderToService(connection);
            connection.Close();

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
        public abstract void createCityTable(DbConnection connection);
        public abstract void createCityDistrictsTable(DbConnection connection);
        public abstract void createAddressTable(DbConnection connection);
        public abstract void createWorkerTable(DbConnection connection);
        public abstract void createFeedbackTable(DbConnection connection);
        public abstract void createOrderListTable(DbConnection connection);
        public abstract void createOrderTable(DbConnection connection);
        public abstract void createProfCategoryTable(DbConnection connection);
        public abstract void createProfessionTable(DbConnection connection);
        public abstract void createServiceTable(DbConnection connection);
        public abstract void createOrderToService(DbConnection connection);
     
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
