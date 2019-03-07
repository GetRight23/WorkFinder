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
			List<string> feedBacks = new List<string>();
			List<string> names = new List<string>();

			List<Worker> listWorkers = Storage.WorkerDao.selectEntities();

			fileLoader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\feedbacks.txt");
			feedBacks.AddRange(fileLoader.filter(fileLoader.entities));

			fileLoader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\russian_names.txt");
			names.AddRange(fileLoader.entities);

			for (int i = 0; i < listWorkers.Count; i++)
			{
				Feedback feedback = new Feedback()
				{
					Name = names[Random.Next(0, names.Count)],
					Patronymic = names[Random.Next(0, names.Count)],
					GradeValue = Random.Next(1, 5),
					Date = DateTime.Now,
					Text = feedBacks[Random.Next(0, feedBacks.Count)],
					IdWorker = listWorkers[Random.Next(0, listWorkers.Count)].Id
				};
				Storage.FeedbackDao.insertEntity(feedback);
			}
		}
	}
}
