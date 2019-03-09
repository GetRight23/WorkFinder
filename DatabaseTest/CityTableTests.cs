using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseConfiguration;
using DatabaseDao;
using Models;
using System.Data.Common;
using System.Collections.Generic;

namespace DatabaseTest
{
	[TestClass]
	public class CityTableTests
	{
		private static Storage Storage { get; set; }
		private static DbConnection Connection { get; set; }
		private static City city = null;

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
		}

		[ClassCleanup]
		public static void cleanup()
		{
			var command = Connection.CreateCommand();
			command.CommandText = "drop table if exists city";
			command.ExecuteNonQuery();
			Connection.Close();
		}

		[TestMethod]
		public void runTests()
		{
			insertCityTest();
			selectCityTest();
			updateCityTest();
			deleteCitytest();
		}

		public void insertCityTest()
		{
			city = new City() { Name = "Kyiv" };
			Assert.IsNotNull(city);

			int id = Storage.CityDao.insertEntity(city);

			Assert.IsTrue(id != 0);
		}

		public void selectCityTest()
		{
			List<City> cities = Storage.CityDao.selectEntities();
			Assert.IsTrue(cities.Count == 1);
		}

		public void updateCityTest()
		{
			Assert.IsNotNull(city);
			city.Name = "London";

			bool result = Storage.CityDao.updateEntity(city);

			Assert.IsTrue(result);
			Assert.IsTrue(Storage.CityDao.selectEntityById(city.Id).Name == "London");
		}

		public void deleteCitytest()
		{
			Assert.IsNotNull(city);
			bool result = Storage.CityDao.deleteEntityById(city.Id);

			Assert.IsTrue(result);
			Assert.IsTrue(Storage.CityDao.selectEntities().Count == 0);
		}
	}
}
