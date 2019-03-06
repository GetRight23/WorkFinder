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

			List<string> names = new List<string>();
			List<string> lastNames = new List<string>();

			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\russian_names.txt");
			names.AddRange(loader.filter(loader.entities));

			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\russian_surnames.txt");
			lastNames.AddRange(loader.filter(loader.entities));

			Random rand = new Random();

			List<Worker> workers = new List<Worker>();

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
				workers.Add(worker);			
			}
			Storage.WorkerDao.insertEntities(workers);
			Console.WriteLine("Worker added!");
		}
	}
}
