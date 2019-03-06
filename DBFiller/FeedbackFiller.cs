using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFiller
{
	class FeedbackFiller : DBFiller
	{
		public FeedbackFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			List<Address> addresses = Storage.AddressDao.selectEntities();
			FileLoader loader = new FileLoader();
			loader.filter(loader.entities);

			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\feedbacks.txt");
			List<string> feedBacks = new List<string>();
			feedBacks.AddRange(loader.entities);

			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\russian_names.txt");
			List<string> names = new List<string>();
			names.AddRange(loader.entities);

			Random rand = new Random();

			List<Worker> listWorkers = Storage.WorkerDao.selectEntities();

			for (int i = 0; i < listWorkers.Count; i++)
			{
				Feedback feedback = new Feedback()
				{
					Name = names[rand.Next(0, names.Count)],
					Patronymic = names[rand.Next(0, names.Count)],
					GradeValue = rand.Next(1, 5),
					Date = DateTime.Now,
					Text = feedBacks[rand.Next(0, feedBacks.Count)],
					IdWorker = listWorkers[rand.Next(0, listWorkers.Count)].Id
				};
				Storage.FeedbackDao.insertEntity(feedback);
			}
			Console.WriteLine("Feedback added!");
		}
	}
}
