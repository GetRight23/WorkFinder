using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
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

			try
			{
				List<Worker> listWorkers = Storage.WorkerDao.selectEntities();
				List<Feedback> feedbackList = new List<Feedback>();

				feedBacks = FileLoader.load(@".\res\feedbacks.txt");

				names = FileLoader.load(@".\res\russian_names.txt");

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
					feedbackList.Add(feedback);
				}
				Storage.FeedbackDao.insertEntities(feedbackList);
				Logger.Info("Feedback Table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}			
		}
	}
}
