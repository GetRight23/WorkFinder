using NUnit.Framework;
using Models;
using DatabaseDao;
using DatabaseConfiguration;
using System.Data.Common;
using System.Collections.Generic;

namespace DatabaseTests
{
	class AddressTests
	{
		private static Storage Storage { get; set; }
		private static DbConnection Connection { get; set; }
		private static DatabaseDaoImpl<City> CityDao { get; set; }
		private static DatabaseDaoImpl<CityDistricts> CityDistrictsDao { get; set; }
		private static DatabaseDaoImpl<Address> AddressDao { get; set; }
		private static City city = null;
		private static CityDistricts district = null;
		private static Address address = null;

		[OneTimeSetUp]
		public void Setup()
		{
			Storage = new PostgreStorage(Configuration.TestConnection);

			Assert.IsNotNull(Storage.Connection);
			Assert.IsNotNull(Storage.Database);
			Assert.IsNotNull(Storage.CityDao);
			Assert.IsNotNull(Storage.CityDistrictsDao);
			Assert.IsNotNull(Storage);

			Connection = Storage.Connection;
			CityDao = Storage.CityDao;
			CityDistrictsDao = Storage.CityDistrictsDao;
			AddressDao = Storage.AddressDao;

			Connection.Open();
			Storage.createCityTable();
			Storage.createCityDistrictsTable();
			Storage.createAddressTable();
		}

		[OneTimeTearDown]
		public void CleanUp()
		{
			var command = Connection.CreateCommand();
			command.CommandText =	"drop table if exists address;" +
									"drop table if exists city_districts;" +
									"drop table if exists city";
			command.ExecuteNonQuery();
			Connection.Close();
		}

		[Test, Order(1)]
		public void insertTest()
		{
			city = new City() { Name = "Kyiv" };
			int cityId = CityDao.insertEntity(city);

			district = new CityDistricts() { IdCity = cityId, Name = "South" };
			int districtId = CityDistrictsDao.insertEntity(district);

			address = new Address() { IdCity = cityId, IdCityDistrict = districtId, ApptNum = "5", StreetName = "Lenina" };
			int addressId = AddressDao.insertEntity(address);

			Assert.IsTrue(addressId != 0);
		}

		[Test, Order(2)]
		public void selectTest()
		{
			List<Address> addresses = AddressDao.selectEntities();
			Assert.IsTrue(addresses.Count == 1);
		}

		[Test, Order(3)]
		public void updateTest()
		{
			Assert.IsNotNull(address);
			address.StreetName = "Artema";

			bool result = AddressDao.updateEntity(address);

			Assert.IsTrue(result);
			Assert.IsTrue(AddressDao.selectEntityById(address.Id).StreetName == "Artema");
		}

		[Test, Order(4)]
		public void deleteTest()
		{
			Assert.IsNotNull(address);
			bool result = AddressDao.deleteEntityById(address.Id);

			Assert.IsTrue(result);
			Assert.IsTrue(AddressDao.selectEntities().Count == 0);
		}
	}
}