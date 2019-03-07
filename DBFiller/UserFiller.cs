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
			Random rand = new Random();
			FileLoader loader = new FileLoader();

			List<string> logins = new List<string>();
			List<string> passwords = new List<string>();

			List<User> users = new List<User>();
			List<Worker> workers = Storage.WorkerDao.selectEntities();

			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\logins.txt");
			logins.AddRange(loader.entities);

			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\passwords.txt");
			passwords.AddRange(loader.entities);

			for (int i = 0; i < workers.Count; i++)
			{
				User user = new User()
				{
					Login = logins[rand.Next(0, logins.Count)],
					Password = passwords[rand.Next(0, passwords.Count)],
					IdWorker = workers[rand.Next(0, workers.Count)].Id
				};
				users.Add(user);
			}
			Storage.UserDao.insertEntities(users);
		}
	}
}
