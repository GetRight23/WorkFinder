using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFiller
{
	class UserFiller : DBFiller
	{
		public UserFiller(Storage storage) : base(storage) {}
		public override void fillEntities()
		{
			List<string> logins = new List<string>();
			List<string> passwords = new List<string>();

			List<User> users = new List<User>();
			List<Worker> workers = Storage.WorkerDao.selectEntities();

			fileLoader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\logins.txt");
			logins.AddRange(fileLoader.entities);

			fileLoader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\passwords.txt");
			passwords.AddRange(fileLoader.entities);

			for (int i = 0; i < workers.Count; i++)
			{
				User user = new User()
				{
					Login = logins[Random.Next(0, logins.Count)],
					Password = passwords[Random.Next(0, passwords.Count)],
					IdWorker = workers[Random.Next(0, workers.Count)].Id
				};
				users.Add(user);
			}
			Storage.UserDao.insertEntities(users);
		}
	}
}
