using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;

namespace DatabaseFiller
{
	class WorkerFiller : DBFiller
	{
		public WorkerFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			try
			{
				List<string> names = new List<string>();
				List<string> lastNames = new List<string>();

				List<User> users = Storage.UserDao.selectEntities();
				List<Address> addresses = Storage.AddressDao.selectEntities();
				List<Worker> workers = new List<Worker>();

				names = FileLoader.load(@".\res\russian_names.txt");
				lastNames = FileLoader.load(@".\res\russian_surnames.txt");

				int workersSize = users.Count / 2;
				for (int i = 0; i < workersSize; i++)
				{
					int firstPart = Random.Next(10, 99);
					int secondPart = Random.Next(100, 999);
					int thirdPart = Random.Next(1000, 9999);
					Worker worker = new Worker()
					{
						PhoneNumber = $"+380{firstPart}{secondPart}{thirdPart}",
						Info = "test",
						IdAddress = addresses[Random.Next(0, addresses.Count)].Id,
						Name = names[Random.Next(0, names.Count)],
						LastName = lastNames[Random.Next(0, lastNames.Count)],
						IdUser = users[i].Id
					};
					workers.Add(worker);
				}
				Storage.WorkerDao.insertEntities(workers);
				Logger.Info("Worker table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				Logger.Error("Worker filler filling failed");
			}		
		}
	}
}
