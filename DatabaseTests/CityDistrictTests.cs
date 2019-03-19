using NUnit.Framework;
using Models;
using DatabaseDao;
using DatabaseConfiguration;
using System.Data.Common;
using System.Collections.Generic;

namespace DatabaseTests
{
	class CityDistrictTests
	{
		private static Storage Storage { get; set; }
		private static DbConnection Connection { get; set; }
		private static DatabaseDaoImpl<City> CityDao { get; set; }
		private static DatabaseDaoImpl<CityDistricts> CityDistrictsDao { get; set; }
		private static City city = null;
		private static CityDistricts district = null;

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

			Connection.Open();
			Storage.createCityTable();
			Storage.createCityDistrictsTable();
		}

		[OneTimeTearDown]
		public void CleanUp()
		{
			var command = Connection.CreateCommand();
			command.CommandText = "drop table if exists city_districts;" +
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

			Assert.IsTrue(districtId != 0);
		}

		[Test, Order(2)]
		public void selectTest()
		{
			List<CityDistricts> districts = CityDistrictsDao.selectEntities();
			Assert.IsTrue(districts.Count == 1);
		}

		[Test, Order(3)]
		public void updateTest()
		{
			Assert.IsNotNull(district);
			district.Name = "North";

			bool result = CityDistrictsDao.updateEntity(district);

			Assert.IsTrue(result);
			Assert.IsTrue(CityDistrictsDao.selectEntityById(district.Id).Name == "North");
		}

		[Test, Order(4)]
		public void deleteTest()
		{
			Assert.IsNotNull(district);
			bool result = CityDistrictsDao.deleteEntityById(district.Id);

			Assert.IsTrue(result);
			Assert.IsTrue(CityDistrictsDao.selectEntities().Count == 0);
		}
	}
}