using NUnit.Framework;
using Models;
using DatabaseDao;
using DatabaseConfiguration;
using System.Data.Common;
using System.Collections.Generic;

namespace DatabaseTests
{
	class CityTests
	{
		private static Storage Storage { get; set; }
		private static DbConnection Connection { get; set; }
		private static DatabaseDaoImpl<City> CityDao { get; set; }
		private static City city = null;

		[OneTimeSetUp]
		public void Setup()
		{
			Storage = new PostgreStorage(Configuration.TestConnection);

			Assert.IsNotNull(Storage.Connection);
			Assert.IsNotNull(Storage.Database);
			Assert.IsNotNull(Storage.CityDao);
			Assert.IsNotNull(Storage);

			Connection = Storage.Connection;
			CityDao = Storage.CityDao;

			Connection.Open();
			Storage.createCityTable();
		}

		[OneTimeTearDown]
		public void CleanUp()
		{
			var command = Connection.CreateCommand();
			command.CommandText = "drop table if exists city";
			command.ExecuteNonQuery();
			Connection.Close();
		}

		[Test, Order(1)]
		public void insertCityTest()
		{
			city = new City() { Name = "Kyiv" };
			Assert.IsNotNull(city);

			int id = CityDao.insertEntity(city);

			Assert.IsTrue(id != 0);
		}

		[Test, Order(2)]
		public void selectCityTest()
		{
			List<City> cities = CityDao.selectEntities();
			Assert.IsTrue(cities.Count == 1);
		}

		[Test, Order(3)]
		public void updateCityTest()
		{
			Assert.IsNotNull(city);
			city.Name = "London";

			bool result = CityDao.updateEntity(city);

			Assert.IsTrue(result);
			Assert.IsTrue(CityDao.selectEntityById(city.Id).Name == "London");
		}

		[Test, Order(4)]
		public void deleteCitytest()
		{
			Assert.IsNotNull(city);
			bool result = CityDao.deleteEntityById(city.Id);

			Assert.IsTrue(result);
			Assert.IsTrue(CityDao.selectEntities().Count == 0);
		}
	}
}