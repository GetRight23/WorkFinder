using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;

namespace DatabaseFiller
{
	class ProfessinonsToWorkersFiller : DBFiller
	{
		public ProfessinonsToWorkersFiller(Storage storage) : base(storage) { }

		public override void fillEntities()
		{
			try
			{
				List<Profession> professions = Storage.ProfessionDao.selectEntities();
				List<Worker> workers = Storage.WorkerDao.selectEntities();

				List<ProfessionToWorker> professionToWorkers = new List<ProfessionToWorker>();
				for (int i = 0; i < workers.Count; i++)
				{
					int professionsPerWorker = Random.Next(1, professions.Count);
					for (int j = 0; j < professionsPerWorker; j++)
					{
						ProfessionToWorker professionToWorker = new ProfessionToWorker()
						{
							IdProfession = professions[j].Id,
							IdWorker = workers[i].Id
						};
						professionToWorkers.Add(professionToWorker);
					}
					Storage.ProfessionToWorkerDao.insertRelationships(professionToWorkers);
					professionToWorkers.Clear();
				}
				Logger.Info("Professinons to workers table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.InnerException.Message);
				Logger.Error("Professinons to workers filler filling failed");
			}
		}
	}
}
