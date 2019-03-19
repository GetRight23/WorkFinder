using NUnit.Framework;
using Models;
using DatabaseDao;
using DatabaseConfiguration;
using System.Data.Common;
using System.Collections.Generic;

namespace DatabaseTests
{
	class UserTest
	{
		private static Storage Storage { get; set; }
		private static DbConnection Connection { get; set; }
		private static DatabaseDaoImpl<User> UserDao { get; set; }
		private static User user = null;

		[OneTimeSetUp]
		public void Setup()
		{
			Storage = new PostgreStorage(Configuration.TestConnection);

			Assert.IsNotNull(Storage.Connection);
			Assert.IsNotNull(Storage.Database);
			Assert.IsNotNull(Storage.UserDao);
			Assert.IsNotNull(Storage);

			Connection = Storage.Connection;
			UserDao = Storage.UserDao;

			Connection.Open();
			Storage.createUserTable();
		}

		[OneTimeTearDown]
		public void CleanUp()
		{
			var command = Connection.CreateCommand();
			command.CommandText = "drop table if exists user_table;";
			command.ExecuteNonQuery();
			Connection.Close();
		}

		[Test, Order(1)]
		public void insertTest()
		{
			user = new User() { Login = "oleg", Password = "test" };
			int userId = UserDao.insertEntity(user);

			Assert.IsTrue(userId != 0);
		}

		[Test, Order(2)]
		public void selectTest()
		{
			List<User> users = UserDao.selectEntities();
			Assert.IsTrue(users.Count == 1);
		}

		[Test, Order(3)]
		public void updateTest()
		{
			Assert.IsNotNull(user);
			user.Login = "vlad";

			bool result = UserDao.updateEntity(user);

			Assert.IsTrue(result);
			Assert.IsTrue(UserDao.selectEntityById(user.Id).Login == "vlad");
		}

		[Test, Order(4)]
		public void deleteTest()
		{
			Assert.IsNotNull(user);
			bool result = UserDao.deleteEntityById(user.Id);

			Assert.IsTrue(result);
			Assert.IsTrue(UserDao.selectEntities().Count == 0);
		}
	}
}