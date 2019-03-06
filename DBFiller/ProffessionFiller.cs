using System;
using System.Collections.Generic;
using System.Text;
using DatabaseDao;
using Models;

namespace DBFiller
{
	class ProffessionFiller : DBFiller
	{
		public ProffessionFiller(Storage storage) : base(storage){}

		public override void fillEntities()
		{
			FileLoader fileLoader = new FileLoader();
			List<string> professionList = new List<string>();
			fileLoader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\professions.txt");
			professionList.AddRange(fileLoader.entities);

			List<Worker> workers = Storage.WorkerDao.selectEntities();
			List<ProfCategory> profCategories = Storage.ProfCategoryDao.selectEntities();

			List<Profession> professions = new List<Profession>();

			Random rand = new Random();

			for (int i = 0; i < professionList.Count; i++)
			{
				Profession profession = new Profession()
				{
					Name = professionList[rand.Next(0, professionList.Count)],
					IdWorker = workers[rand.Next(0, workers.Count)].Id,
					IdProfCategory = profCategories[rand.Next(0, profCategories.Count)].Id
				};
				professions.Add(profession);
			}
			Storage.ProfesionDao.insertEntities(professions);
		}
	}
}
