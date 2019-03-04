using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFiller
{
	public class WorkerFiller : DBFiller
	{
		public WorkerFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			const int size = 10;
			List<Address> addresses = Storage.AddressDao.selectEntities();
			FileLoader loader = new FileLoader();

			List<string> lastNames = loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\russian_surnames.txt");
			List<string> names = loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\russian_names.txt");

			Random rand = new Random();

			for (int i = 0; i < size; i++)
			{
				int firstPart = rand.Next(10, 99);
				int secondPart = rand.Next(100, 999);
				int thirdPart = rand.Next(1000, 9999);
				Worker worker = new Worker()
				{
					PhoneNumber = $"+380{firstPart}{secondPart}{thirdPart}",
					Info = "test",
					IdAddress = addresses[rand.Next(0, addresses.Count)].Id,
					Name = names[rand.Next(0, names.Count)],
					LastName = lastNames[rand.Next(0, lastNames.Count)]
				};
				Storage.WorkerDao.insertEntity(worker);
			}
			Console.WriteLine("Worker added!");
		}
	}
}
