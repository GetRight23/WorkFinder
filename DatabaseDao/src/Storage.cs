using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Models;

namespace DatabaseDao
{
	public abstract class Storage
	{
		public static ApplicationContext m_ctx = new ApplicationContext();

		public Storage(DbConnection connection)
		{
			connection.Open();
			createCityTable(connection);
			createCityDistrictsTable(connection);
			createAddressTable(connection);
			createWorkerTable(connection);
			createFeedbackTable(connection);
			createOrdersListTable(connection);
			createOrderTable(connection);
			createProfCategoryTable(connection);
			createProfessionTable(connection);
			createServiceTable(connection);
			createOrderToService(connection);
			createUserTable(connection);
			createPhotoTable(connection);
			connection.Close();

			AddressDao = new DatabaseDao<Address>(m_ctx, m_ctx.Address);
			CityDao = new DatabaseDao<City>(m_ctx, m_ctx.City);
			CityDistrictsDao = new DatabaseDao<CityDistricts>(m_ctx, m_ctx.CityDistricts);
			FeedbackDao = new DatabaseDao<Feedback>(m_ctx, m_ctx.Feedback);
			OrderListDao = new DatabaseDao<Orderslist>(m_ctx, m_ctx.Orderslist);
			OrderTableDao = new DatabaseDao<OrderTable>(m_ctx, m_ctx.OrderTable);
			ProfCategoryDao = new DatabaseDao<ProfCategory>(m_ctx, m_ctx.ProfCategory);
			ProfesionDao = new DatabaseDao<Profession>(m_ctx, m_ctx.Profession);
			ServiceDao = new DatabaseDao<Service>(m_ctx, m_ctx.Service);
			WorkerDao = new DatabaseDao<Worker>(m_ctx, m_ctx.Worker);
			OrderToServiceDao = new OrderToServiceDao(m_ctx, m_ctx.OrderToService);
			UserDao = new DatabaseDao<User>(m_ctx, m_ctx.User);
			PhotoDao = new DatabaseDao<Photo>(m_ctx, m_ctx.Photo);
		}
		public abstract void createCityTable(DbConnection connection);
		public abstract void createCityDistrictsTable(DbConnection connection);
		public abstract void createAddressTable(DbConnection connection);
		public abstract void createWorkerTable(DbConnection connection);
		public abstract void createFeedbackTable(DbConnection connection);
		public abstract void createOrdersListTable(DbConnection connection);
		public abstract void createOrderTable(DbConnection connection);
		public abstract void createProfCategoryTable(DbConnection connection);
		public abstract void createProfessionTable(DbConnection connection);
		public abstract void createServiceTable(DbConnection connection);
		public abstract void createOrderToService(DbConnection connection);
		public abstract void createUserTable(DbConnection connection);
		public abstract void createPhotoTable(DbConnection connection);


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
		public DatabaseDao<User> UserDao { get; private set; }
		public DatabaseDao<Photo> PhotoDao { get; private set; }
		public OrderToServiceDao OrderToServiceDao { get; set; }


	}
}
