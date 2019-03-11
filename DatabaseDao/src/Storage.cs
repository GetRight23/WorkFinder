using System.Data.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Models;

namespace DatabaseDao
{
	public abstract class Storage
	{
		private DBContext m_dbContext = null;

		public DbConnection Connection { get; private set; }

		public DatabaseFacade Database { get; private set; }

		public Storage(DbConnection connection)
		{
			Connection = connection;
			m_dbContext = new DBContext(connection.ConnectionString);
			Database = m_dbContext.Database;

			AddressDao = new DatabaseDao<Address>(m_dbContext, m_dbContext.Address);
			CityDao = new DatabaseDao<City>(m_dbContext, m_dbContext.City);
			CityDistrictsDao = new DatabaseDao<CityDistricts>(m_dbContext, m_dbContext.CityDistricts);
			FeedbackDao = new DatabaseDao<Feedback>(m_dbContext, m_dbContext.Feedback);
			OrderListDao = new DatabaseDao<Orderslist>(m_dbContext, m_dbContext.Orderslist);
			OrderTableDao = new DatabaseDao<OrderTable>(m_dbContext, m_dbContext.OrderTable);
			ProfCategoryDao = new DatabaseDao<ProfCategory>(m_dbContext, m_dbContext.ProfCategory);
			ProfessionDao = new DatabaseDao<Profession>(m_dbContext, m_dbContext.Profession);
			ServiceDao = new DatabaseDao<Service>(m_dbContext, m_dbContext.Service);
			WorkerDao = new DatabaseDao<Worker>(m_dbContext, m_dbContext.Worker);
			OrderToServiceDao = new OrderToServiceDao(m_dbContext, m_dbContext.OrderToService);
			ProfessionToWorkerDao = new ProfessionToWorkerDao(m_dbContext, m_dbContext.ProfessionToWorker);
			UserDao = new DatabaseDao<User>(m_dbContext, m_dbContext.User);
			PhotoDao = new DatabaseDao<Photo>(m_dbContext, m_dbContext.Photo);
		}

		public void createTables()
		{
			Connection.Open();
			createCityTable();
			createCityDistrictsTable();
			createAddressTable();
			createUserTable();
			createPhotoTable();
			createProfCategoryTable();
			createProfessionTable();
			createServiceTable();
			createWorkerTable();
			createFeedbackTable();
			createOrdersListTable();
			createOrderTable();
			createOrderToService();
			createProfessionToWorker();
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
