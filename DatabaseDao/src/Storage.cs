using System.Data.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Models;

namespace DatabaseDao
{
	public abstract class Storage
	{
		private DBContext context = null;

		public DbConnection Connection { get; private set; }

		public DatabaseFacade Database { get; private set; }

		public Storage(DbConnection connection)
		{
			Connection = connection;
			context = new DBContext(connection.ConnectionString);
			Database = context.Database;

			AddressDao = new DatabaseDaoImpl<Address>(context, context.Address);
			CityDao = new DatabaseDaoImpl<City>(context, context.City);
			CityDistrictsDao = new DatabaseDaoImpl<CityDistricts>(context, context.CityDistricts);
			FeedbackDao = new DatabaseDaoImpl<Feedback>(context, context.Feedback);
			OrderListDao = new DatabaseDaoImpl<OrdersList>(context, context.OrdersList);
			OrderDao = new DatabaseDaoImpl<Order>(context, context.Order);
			ProfessionCategoryDao = new DatabaseDaoImpl<ProfessionCategory>(context, context.ProfCategory);
			ProfessionDao = new DatabaseDaoImpl<Profession>(context, context.Profession);
			ServiceDao = new DatabaseDaoImpl<Service>(context, context.Service);
			WorkerDao = new DatabaseDaoImpl<Worker>(context, context.Worker);
			OrderToServiceDao = new OrderToServiceDao(context, context.OrderToService);
			ProfessionToWorkerDao = new ProfessionToWorkerDao(context, context.ProfessionToWorker);
			UserDao = new DatabaseDaoImpl<User>(context, context.User);
			PhotoDao = new DatabaseDaoImpl<Photo>(context, context.Photo);
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
