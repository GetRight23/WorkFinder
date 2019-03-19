using NUnit.Framework;
using Models;
using DatabaseDao;
using DatabaseConfiguration;
using System.Data.Common;
using System.Collections.Generic;

namespace DatabaseTests
{
	class WorkerTests
	{
		private static Storage Storage { get; set; }
		private static DbConnection Connection { get; set; }
		private static DatabaseDaoImpl<City> CityDao { get; set; }
		private static DatabaseDaoImpl<CityDistricts> CityDistrictsDao { get; set; }
		private static DatabaseDaoImpl<Address> AddressDao { get; set; }
		private static DatabaseDaoImpl<Worker> WorkerDao { get; set; }
		private static DatabaseDaoImpl<User> UserDao { get; set; }
		private static City city = null;
		private static Worker worker = null;
		private static CityDistricts district = null;
		private static Address address = null;
		private static User user = null;

		[OneTimeSetUp]
		public void Setup()
		{
			Storage = new PostgreStorage(Configuration.TestConnection);

			Assert.IsNotNull(Storage.Connection);
			Assert.IsNotNull(Storage.Database);
			Assert.IsNotNull(Storage.CityDao);
			Assert.IsNotNull(Storage.CityDistrictsDao);
			Assert.IsNotNull(Storage.WorkerDao);
			Assert.IsNotNull(Storage.UserDao);
			Assert.IsNotNull(Storage);

			Connection = Storage.Connection;
			CityDao = Storage.CityDao;
			CityDistrictsDao = Storage.CityDistrictsDao;
			AddressDao = Storage.AddressDao;
			UserDao = Storage.UserDao;
			WorkerDao = Storage.WorkerDao;

			Connection.Open();
			Storage.createCityTable();
			Storage.createCityDistrictsTable();
			Storage.createAddressTable();
			Storage.createUserTable();
			Storage.createWorkerTable();
		}

		[OneTimeTearDown]
		public void CleanUp()
		{
			var command = Connection.CreateCommand();
			command.CommandText =	"drop table if exists worker;" +
									"drop table if exists address;" +
									"drop table if exists city_districts;" +
									"drop table if exists city;" +
									"drop table if exists user_table";
			command.ExecuteNonQuery();
			Connection.Close();
		}

		[Test, Order(1)]
		public void insertTest()
		{
			city = new City() { Name = "Kyiv" };
			int cityId = CityDao.insertEntity(city);

			Assert.IsTrue(cityId != 0);

			district = new CityDistricts() { IdCity = cityId, Name = "South" };
			int districtId = CityDistrictsDao.insertEntity(district);

			Assert.IsTrue(districtId != 0);

			address = new Address()
			{
				IdCity = cityId,
				IdCityDistrict = districtId,
				ApptNum = "5",
				StreetName = "Lenina"
			};

			int addressId = AddressDao.insertEntity(address);

			Assert.IsTrue(addressId != 0);

			user = new User()
			{
				Login = "yakrut",
				Password = "124124124",
			};
			int userId = UserDao.insertEntity(user);

			Assert.IsTrue(userId != 0);

			worker = new Worker()
			{
				IdAddress = addressId,
				Info = "noname",
				Name = "Ivan",
				LastName = "Ivanov",
				PhoneNumber = "1241242",
				IdUser = userId
			};
			int workerId = WorkerDao.insertEntity(worker);

			Assert.IsTrue(workerId != 0);
		}

		[Test, Order(2)]
		public void selectTest()
		{
			List<Worker> workers = WorkerDao.selectEntities();
			Assert.IsTrue(workers.Count == 1);
		}

		[Test, Order(3)]
		public void updateTest()
		{
			Assert.IsNotNull(worker);
			worker.Name = "Vasiliy";

			bool result = WorkerDao.updateEntity(worker);

			Assert.IsTrue(result);
			Assert.IsTrue(WorkerDao.selectEntityById(worker.Id).Name == "Vasiliy");
		}

		[Test, Order(4)]
		public void deleteTest()
		{
			Assert.IsNotNull(worker);
			bool result = WorkerDao.deleteEntityById(worker.Id);

			Assert.IsTrue(result);
			Assert.IsTrue(WorkerDao.selectEntities().Count == 0);
		}
	}
}
