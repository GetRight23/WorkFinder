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
		private static DatabaseDaoImpl<ProfessionCategory> ProfessionCategoryDao { get; set; }
		private static DatabaseDaoImpl<Profession> ProfessionDao { get; set; }
		private static ProfessionCategory professionCategory = null;
        private static Profession profession = null;

        [OneTimeSetUp]
        public void Setup()
        {
            Storage = new PostgreStorage(Configuration.TestConnection);

            Assert.IsNotNull(Storage.Connection);
            Assert.IsNotNull(Storage.Database);
            Assert.IsNotNull(Storage.ProfessionCategoryDao);
            Assert.IsNotNull(Storage.ProfessionDao);
            Assert.IsNotNull(Storage);

            Connection = Storage.Connection;
			ProfessionCategoryDao = Storage.ProfessionCategoryDao;
			ProfessionDao = Storage.ProfessionDao;

			Connection.Open();
            Storage.createProfCategoryTable();
            Storage.createProfessionTable();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            var command = Connection.CreateCommand();
            command.CommandText = "drop table if exists prof_category cascade;" +
								  "drop table if exists profession cascade;";
            command.ExecuteNonQuery();
            Connection.Close();
        }

        [Test, Order(1)]
        public void insertTest()
        {
            professionCategory = new ProfessionCategory() { Name = "Teacher" };
            int professionCategoryId = ProfessionCategoryDao.insertEntity(professionCategory);

			Assert.IsTrue(professionCategoryId != 0);

			profession = new Profession() { Name = "Math", IdProfCategory = professionCategoryId };
            int professionId = ProfessionDao.insertEntity(profession);

            Assert.IsTrue(professionId != 0);
        }

		[Test, Order(2)]
		public void selectTest()
		{
			List<Profession> professions = ProfessionDao.selectEntities();
			Assert.IsTrue(professions.Count == 1);
		}

		[Test, Order(3)]
		public void updateTest()
		{
			Assert.IsNotNull(profession);
			profession.Name = "Engigneer";

			bool result = ProfessionDao.updateEntity(profession);

			Assert.IsTrue(result);
			Assert.IsTrue(ProfessionDao.selectEntityById(profession.Id).Name == "Engigneer");
		}

		[Test, Order(4)]
		public void deleteTest()
		{
			Assert.IsNotNull(profession);
			bool result = ProfessionDao.deleteEntityById(profession.Id);

			Assert.IsTrue(result);
			Assert.IsTrue(ProfessionDao.selectEntities().Count == 0);
		}
	}
}
