using System.Data.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Models;

namespace DatabaseDao
{
	public abstract class Storage
	{
		private DBContext dbContext = null;

		public DbConnection Connection { get; private set; }

		public DatabaseFacade Database { get; private set; }

		public Storage(DbConnection connection)
		{
			Connection = connection;
			dbContext = new DBContext(connection.ConnectionString);
			Database = dbContext.Database;

			AddressDao = new DatabaseDaoImpl<Address>(dbContext, dbContext.Address);
			CityDao = new DatabaseDaoImpl<City>(dbContext, dbContext.City);
			CityDistrictsDao = new DatabaseDaoImpl<CityDistricts>(dbContext, dbContext.CityDistricts);
			FeedbackDao = new DatabaseDaoImpl<Feedback>(dbContext, dbContext.Feedback);
			OrderListDao = new DatabaseDaoImpl<OrdersList>(dbContext, dbContext.OrdersList);
			OrderDao = new DatabaseDaoImpl<Order>(dbContext, dbContext.Order);
			ProfessionCategoryDao = new DatabaseDaoImpl<ProfessionCategory>(dbContext, dbContext.ProfCategory);
			ProfessionDao = new DatabaseDaoImpl<Profession>(dbContext, dbContext.Profession);
			ServiceDao = new DatabaseDaoImpl<Service>(dbContext, dbContext.Service);
			WorkerDao = new DatabaseDaoImpl<Worker>(dbContext, dbContext.Worker);
			OrderToServiceDao = new OrderToServiceDao(dbContext, dbContext.OrderToService);
			ProfessionToWorkerDao = new ProfessionToWorkerDao(dbContext, dbContext.ProfessionToWorker);
			UserDao = new DatabaseDaoImpl<User>(dbContext, dbContext.User);
			PhotoDao = new DatabaseDaoImpl<Photo>(dbContext, dbContext.Photo);
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


		public DatabaseDaoImpl<Address> AddressDao { get; private set; }
		public DatabaseDaoImpl<City> CityDao { get; private set; }
		public DatabaseDaoImpl<CityDistricts> CityDistrictsDao { get; private set; }
		public DatabaseDaoImpl<Feedback> FeedbackDao { get; private set; }
		public DatabaseDaoImpl<OrdersList> OrderListDao { get; private set; }
		public DatabaseDaoImpl<Order> OrderDao { get; private set; }
		public DatabaseDaoImpl<ProfessionCategory> ProfessionCategoryDao { get; private set; }
		public DatabaseDaoImpl<Profession> ProfessionDao { get; private set; }
		public DatabaseDaoImpl<Service> ServiceDao { get; private set; }
		public DatabaseDaoImpl<Worker> WorkerDao { get; private set; }
		public DatabaseDaoImpl<User> UserDao { get; private set; }
		public DatabaseDaoImpl<Photo> PhotoDao { get; private set; }
		public OrderToServiceDao OrderToServiceDao { get; set; }
		public ProfessionToWorkerDao ProfessionToWorkerDao { get; set; }

}
}
