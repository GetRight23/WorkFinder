using System.Data.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Models;

namespace DatabaseDao
{
	public abstract class Storage
	{
		private ApplicationContext m_ctx = null;

		public DbConnection Connection { get; private set; }

		public DatabaseFacade Database { get; private set; }

		public Storage(DbConnection connection)
		{
			Connection = connection;
			m_ctx = new ApplicationContext(connection.ConnectionString);
			Database = m_ctx.Database;

			AddressDao = new DatabaseDao<Address>(m_ctx, m_ctx.Address);
			CityDao = new DatabaseDao<City>(m_ctx, m_ctx.City);
			CityDistrictsDao = new DatabaseDao<CityDistricts>(m_ctx, m_ctx.CityDistricts);
			FeedbackDao = new DatabaseDao<Feedback>(m_ctx, m_ctx.Feedback);
			OrderListDao = new DatabaseDao<Orderslist>(m_ctx, m_ctx.Orderslist);
			OrderTableDao = new DatabaseDao<OrderTable>(m_ctx, m_ctx.OrderTable);
			ProfCategoryDao = new DatabaseDao<ProfCategory>(m_ctx, m_ctx.ProfCategory);
			ProfessionDao = new DatabaseDao<Profession>(m_ctx, m_ctx.Profession);
			ServiceDao = new DatabaseDao<Service>(m_ctx, m_ctx.Service);
			WorkerDao = new DatabaseDao<Worker>(m_ctx, m_ctx.Worker);
			OrderToServiceDao = new OrderToServiceDao(m_ctx, m_ctx.OrderToService);
			ProfessionToWorkerDao = new ProfessionToWorkerDao(m_ctx, m_ctx.ProfessionToWorker);
			UserDao = new DatabaseDao<User>(m_ctx, m_ctx.User);
			PhotoDao = new DatabaseDao<Photo>(m_ctx, m_ctx.Photo);
		}

		public void createTables()
		{
			Connection.Open();
			createCityTable();
			createCityDistrictsTable();
			createAddressTable();
			createWorkerTable();
			createFeedbackTable();
			createOrdersListTable();
			createOrderTable();
			createProfCategoryTable();
			createProfessionTable();
			createServiceTable();
			createOrderToService();
			createProfessionToWorker();
			createUserTable();
			createPhotoTable();
			Connection.Close();
		}
		public abstract void createCityTable();
		public abstract void createCityDistrictsTable();
		public abstract void createAddressTable();
		public abstract void createWorkerTable();
		public abstract void createFeedbackTable();
		public abstract void createOrdersListTable();
		public abstract void createOrderTable();
		public abstract void createProfCategoryTable();
		public abstract void createProfessionTable();
		public abstract void createServiceTable();
		public abstract void createOrderToService();
		public abstract void createUserTable();
		public abstract void createPhotoTable();
		public abstract void createProfessionToWorker();


		public DatabaseDao<Address> AddressDao { get; private set; }
		public DatabaseDao<City> CityDao { get; private set; }
		public DatabaseDao<CityDistricts> CityDistrictsDao { get; private set; }
		public DatabaseDao<Feedback> FeedbackDao { get; private set; }
		public DatabaseDao<Orderslist> OrderListDao { get; private set; }
		public DatabaseDao<OrderTable> OrderTableDao { get; private set; }
		public DatabaseDao<ProfCategory> ProfCategoryDao { get; private set; }
		public DatabaseDao<Profession> ProfessionDao { get; private set; }
		public DatabaseDao<Service> ServiceDao { get; private set; }
		public DatabaseDao<Worker> WorkerDao { get; private set; }
		public DatabaseDao<User> UserDao { get; private set; }
		public DatabaseDao<Photo> PhotoDao { get; private set; }
		public OrderToServiceDao OrderToServiceDao { get; set; }
		public ProfessionToWorkerDao ProfessionToWorkerDao { get; set; }

}
}
