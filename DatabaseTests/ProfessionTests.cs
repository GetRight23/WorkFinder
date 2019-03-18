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
        private static DatabaseDao<ProfessionCategory> ProfCategoryDao { get; set; }
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
            ProfCategoryDao = Storage.ProfessionCategoryDao;
            ProfessionDao = Storage.ProfessionDao;

            Connection.Open();
            Storage.createProfCategoryTable();
            Storage.createProfessionTable();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            var command = Connection.CreateCommand();
            command.CommandText = "drop table if exists profession" +
                                    "drop table if exists prof_category;";
            command.ExecuteNonQuery();
            Connection.Close();
        }

        [Test, Order(1)]
        public void insertCityTest()
        {
            professionCategory = new ProfessionCategory() { Name = "Teacher" };
            Assert.IsNotNull(professionCategory);

            int profCategoryId = Storage.ProfessionCategoryDao.insertEntity(professionCategory);

            profession = new Profession() { Name = "Math", IdProfCategory = profCategoryId };
            Assert.IsNotNull(profession);

            int profId = Storage.ProfessionDao.insertEntity(profession);

            Assert.IsTrue(profId != 0);
        }
    }
}
