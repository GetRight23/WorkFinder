using DatabaseDao;
using NUnit.Framework;
using System.Data.Common;
using DatabaseConfiguration;
using Models;
using System.Collections.Generic;

namespace DatabaseTests
{
	class ProfCategoryTests
	{
		private static Storage Storage { get; set; }
		private static DbConnection Connection { get; set; }
		private static ProfessionCategory professionCategory = null;

		[OneTimeSetUp]
		public void Setup()
		{
			Storage = new PostgreStorage(Configuration.TestConnection);

			Assert.IsNotNull(Storage.Connection);
			Assert.IsNotNull(Storage.Database);
			Assert.IsNotNull(Storage.ProfCategoryDao);
			Assert.IsNotNull(Storage);

			Connection = Storage.Connection;

			Connection.Open();
			Storage.createProfCategoryTable();
		}

		[OneTimeTearDown]
		public void CleanUp()
		{
			var command = Connection.CreateCommand();
			command.CommandText = "drop table if exists prof_category";
			command.ExecuteNonQuery();
			Connection.Close();
		}

		[Test, Order(1)]
		public void insertProffesionCategoryTest()
		{
			professionCategory = new ProfessionCategory() { Name = "Teacher" };
			Assert.IsNotNull(professionCategory);

			int id = Storage.ProfCategoryDao.insertEntity(professionCategory);

			Assert.IsTrue(id != 0);
		}

		[Test, Order(2)]
		public void selectProffesionCategoryTest()
		{
			List<ProfessionCategory> cities = Storage.ProfCategoryDao.selectEntities();
			Assert.IsTrue(cities.Count == 1);
		}

		[Test, Order(3)]
		public void updateProffesionCategoryTest()
		{
			Assert.IsNotNull(professionCategory);
			professionCategory.Name = "Engigneer";

			bool result = Storage.ProfCategoryDao.updateEntity(professionCategory);

			Assert.IsTrue(result);
			Assert.IsTrue(Storage.ProfCategoryDao.selectEntityById(professionCategory.Id).Name == "Engigneer");
		}

		[Test, Order(4)]
		public void deleteProffesionCategorytest()
		{
			Assert.IsNotNull(professionCategory);
			bool result = Storage.ProfCategoryDao.deleteEntityById(professionCategory.Id);

			Assert.IsTrue(result);
			Assert.IsTrue(Storage.ProfCategoryDao.selectEntities().Count == 0);
		}
	}
}
