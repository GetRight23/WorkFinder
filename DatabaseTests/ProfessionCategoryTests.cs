using DatabaseDao;
using NUnit.Framework;
using System.Data.Common;
using DatabaseConfiguration;
using Models;
using System.Collections.Generic;

namespace DatabaseTests
{
    class ProfessionCategoryTests
    {
        private static Storage Storage { get; set; }
        private static DbConnection Connection { get; set; }
		private static DatabaseDaoImpl<ProfessionCategory> ProfessionCategoryDao { get; set; }
		private static ProfessionCategory professionCategory = null;

        [OneTimeSetUp]
        public void Setup()
        {
            Storage = new PostgreStorage(Configuration.TestConnection);

            Assert.IsNotNull(Storage.Connection);
            Assert.IsNotNull(Storage.Database);
            Assert.IsNotNull(Storage.ProfessionCategoryDao);
            Assert.IsNotNull(Storage);

            Connection = Storage.Connection;
			ProfessionCategoryDao = Storage.ProfessionCategoryDao;

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
        public void insertTest()
        {
            professionCategory = new ProfessionCategory() { Name = "Teacher" };
            Assert.IsNotNull(professionCategory);

            int id = ProfessionCategoryDao.insertEntity(professionCategory);

            Assert.IsTrue(id != 0);
        }

        [Test, Order(2)]
        public void selectTest()
        {
            List<ProfessionCategory> cities = ProfessionCategoryDao.selectEntities();
            Assert.IsTrue(cities.Count == 1);
        }

        [Test, Order(3)]
        public void updateTest()
        {
            Assert.IsNotNull(professionCategory);
            professionCategory.Name = "Engigneer";

            bool result = ProfessionCategoryDao.updateEntity(professionCategory);

            Assert.IsTrue(result);
            Assert.IsTrue(ProfessionCategoryDao.selectEntityById(professionCategory.Id).Name == "Engigneer");
        }

        [Test, Order(4)]
        public void deleteTest()
        {
            Assert.IsNotNull(professionCategory);
            bool result = ProfessionCategoryDao.deleteEntityById(professionCategory.Id);

            Assert.IsTrue(result);
            Assert.IsTrue(ProfessionCategoryDao.selectEntities().Count == 0);
        }
    }
}
