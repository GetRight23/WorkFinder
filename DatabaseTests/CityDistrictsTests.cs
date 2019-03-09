using NUnit.Framework;
using Models;
using DatabaseDao;
using DatabaseConfiguration;
using System.Data.Common;
using System.Collections.Generic;

namespace DatabaseTests
{
	public class CityDistrictsTests
	{
		private static Storage Storage { get; set; }
		private static DbConnection Connection { get; set; }
		private static City city = null;
		private static CityDistricts district = null;

		[OneTimeSetUp]
		public void Setup()
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
		public void insertCityTest()
		{
			city = new City() { Name = "Kyiv" };
			int cityId = Storage.CityDao.insertEntity(city);

			district = new CityDistricts() { IdCity = cityId, Name = "South" };
			int districtId = Storage.CityDistrictsDao.insertEntity(district);

			Assert.IsTrue(districtId != 0);
		}

		[Test, Order(2)]
		public void selectCityTest()
		{
			List<CityDistricts> districts = Storage.CityDistrictsDao.selectEntities();
			Assert.IsTrue(districts.Count == 1);
		}

		[Test, Order(3)]
		public void updateCityTest()
		{
			Assert.IsNotNull(district);
			district.Name = "North";

			bool result = Storage.CityDistrictsDao.updateEntity(district);

			Assert.IsTrue(result);
			Assert.IsTrue(Storage.CityDistrictsDao.selectEntityById(district.Id).Name == "North");
		}

		[Test, Order(4)]
		public void deleteCitytest()
		{
			Assert.IsNotNull(district);
			bool result = Storage.CityDistrictsDao.deleteEntityById(district.Id);

			Assert.IsTrue(result);
			Assert.IsTrue(Storage.CityDistrictsDao.selectEntities().Count == 0);
		}
	}
}