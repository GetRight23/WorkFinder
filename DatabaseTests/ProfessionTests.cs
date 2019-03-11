using NUnit.Framework;
using Models;
using DatabaseDao;
using DatabaseConfiguration;
using System.Data.Common;
using System.Collections.Generic;

namespace DatabaseTests
{
	class ProfessionTests
	{
		private static Storage Storage { get; set; }
		private static DbConnection Connection { get; set; }
		private static DatabaseDao<Profession> ProfessionDao { get; set; }
		private static DatabaseDao<ProfCategory> ProfCategoryDao { get; set; }
		private static ProfCategory professionCategory = null;
		private static Profession profession = null;

		[OneTimeSetUp]
		public void Setup()
		{
			Storage = new PostgreStorage(Configuration.TestConnection);

			Assert.IsNotNull(Storage.Connection);
			Assert.IsNotNull(Storage.Database);
			Assert.IsNotNull(Storage.ProfCategoryDao);
			Assert.IsNotNull(Storage.ProfessionDao);
			Assert.IsNotNull(Storage);

			Connection = Storage.Connection;
			ProfCategoryDao = Storage.ProfCategoryDao;
			ProfessionDao = Storage.ProfessionDao;

			Connection.Open();
			Storage.createProfCategoryTable();
			Storage.createProfessionTable();
		}

		[OneTimeTearDown]
		public void CleanUp()
		{
			var command = Connection.CreateCommand();
			command.CommandText = "drop table if exists prof_category;" +
									"drop table if exists profession";
			command.ExecuteNonQuery();
			Connection.Close();
		}

		[Test, Order(1)]
		public void insertCityTest()
		{
			professionCategory = new ProfCategory() { Name = "Teacher" };
			Assert.IsNotNull(professionCategory);

			int profCategoryId = Storage.ProfCategoryDao.insertEntity(professionCategory);

			profession = new Profession() { Name = "Math"};
			Assert.IsNotNull(professionCategory);

			int id = Storage.ProfCategoryDao.insertEntity(professionCategory);

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
