using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;

namespace DatabaseFiller
{
	class FeedbackFiller : DBFiller
	{
		public FeedbackFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			try
			{
				List<Worker> workers = Storage.WorkerDao.selectEntities();

				List<string>  names = FileLoader.load(@".\res\russian_names.txt");
				List<string>  feedBacks = FileLoader.load(@".\res\feedbacks.txt");

				List<Feedback> feedbackList = new List<Feedback>();
				for (int i = 0; i < workers.Count; i++)
				{
					Feedback feedback = new Feedback()
					{
						Name = names[Random.Next(0, names.Count)],
						Patronymic = names[Random.Next(0, names.Count)],
						GradeValue = Random.Next(1, 5),
						Date = DateTime.Now,
						Text = feedBacks[Random.Next(0, feedBacks.Count)],
						IdWorker = workers[Random.Next(0, workers.Count)].Id
					};
					feedbackList.Add(feedback);
				}
				Storage.FeedbackDao.insertEntities(feedbackList);
				Logger.Info("Feedback table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}			
		}
	}
}
