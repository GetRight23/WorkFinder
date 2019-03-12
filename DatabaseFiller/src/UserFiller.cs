using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;

namespace DatabaseFiller
{
	class UserFiller : DBFiller
	{
		public UserFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			try
			{
				List<string> logins = new List<string>();
				List<string> passwords = new List<string>();
				List<User> users = new List<User>();

				logins = FileLoader.load(@".\res\logins.txt");
				passwords = FileLoader.load(@".\res\passwords.txt");

				int loginsSize = logins.Count;
				int passwordsSize = passwords.Count;
				for (int i = 0; i < loginsSize; i++)
				{
					User user = new User()
					{
						Login = logins[i],
						Password = passwords[Random.Next(0, passwordsSize)]
					};
					users.Add(user);
				}
				Storage.UserDao.insertEntities(users);
				Logger.Info("User table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.InnerException.Message);
				Logger.Error("User filler filling failed");
			}		
		}
	}
}
