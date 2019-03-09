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
			List<string> professionList = new List<string>();

			List<Worker> workers = Storage.WorkerDao.selectEntities();
			List<ProfCategory> profCategories = Storage.ProfCategoryDao.selectEntities();
			List<Profession> professions = new List<Profession>();

			fileLoader.load(@".\res\professions.txt");
			professionList.AddRange(fileLoader.Entities);

			for (int i = 0; i < professionList.Count; i++)
			{
				Profession profession = new Profession()
				{
					Name = professionList[Random.Next(0, professionList.Count)],
					IdWorker = workers[Random.Next(0, workers.Count)].Id,
					IdProfCategory = profCategories[Random.Next(0, profCategories.Count)].Id
				};
				professions.Add(profession);
			}
			Storage.ProfesionDao.insertEntities(professions);
		}
	}
}
