using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseConfiguration;
using DatabaseDao;
using Models;
using System.Data.Common;
using System.Collections.Generic;

namespace DatabaseTest
{
	[TestClass]
	public class CityDistrictsTableTests
	{
		private static Storage Storage { get; set; }
		private static DbConnection Connection { get; set; }
		private static City city = null;
		private static CityDistricts district = null;

		[ClassInitialize]
		public static void initialize(TestContext context)
		{
			Storage = new PostgreStorage(Configuration.TestConnection);

			Assert.IsNotNull(Storage.Connection);
			Assert.IsNotNull(Storage.Database);
			Assert.IsNotNull(Storage.CityDao);
			Assert.IsNotNull(Storage);

			Connection = Storage.Connection;

			Connection.Open();
			Storage.createCityTable();
			Storage.createCityDistrictsTable();
		}

		[ClassCleanup]
		public static void cleanup()
		{
			var command = Connection.CreateCommand();
			command.CommandText = "drop table if exists city_districts;" +
									"drop table if exists city";
			command.ExecuteNonQuery();
			Connection.Close();
		}
		
		[TestMethod]
		public void runTests()
		{
			insertCityDistrictTest();
			selectCityDistrictTest();
			updateCityDistrictTest();
			deleteCityDistricttest();
		}

		public void insertCityDistrictTest()
		{
			city = new City() { Name = "Kyiv" };
			int city_id = Storage.CityDao.insertEntity(city);
			district = new CityDistricts() { IdCity = city_id, Name = "South" };
			int district_id = Storage.CityDistrictsDao.insertEntity(district);

			Assert.IsTrue(district_id != 0);
		}

		public void selectCityDistrictTest()
		{
			List<CityDistricts> districts = Storage.CityDistrictsDao.selectEntities();
			Assert.IsTrue(districts.Count == 1);
		}

		public void updateCityDistrictTest()
		{
			Assert.IsNotNull(district);
			district.Name = "North";

			bool result = Storage.CityDistrictsDao.updateEntity(district);

			Assert.IsTrue(result);
			Assert.IsTrue(Storage.CityDistrictsDao.selectEntityById(district.Id).Name == "North");
		}

		public void deleteCityDistricttest()
		{
			Assert.IsNotNull(district);
			bool result = Storage.CityDistrictsDao.deleteEntityById(district.Id);

			Assert.IsTrue(result);
			Assert.IsTrue(Storage.CityDistrictsDao.selectEntities().Count == 0);
		}
	}
}
