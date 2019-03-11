using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DatabaseDao;
using Models;

namespace DBFiller
{
	class ProfessionFiller : DBFiller
	{
		public ProfessionFiller(Storage storage) : base(storage){}

		public override void fillEntities()
		{
			try
			{
				List<ProfCategory> profCategories = Storage.ProfCategoryDao.selectEntities();

				List<string >professionNames = FileLoader.load(@".\res\professions.txt");

				int professionsSize = professionNames.Count;
				int profCategoriesSize = profCategories.Count;
				List<Profession> professions = new List<Profession>();
				for (int i = 0; i < profCategoriesSize; i++)
				{
					int professionsPerCategory = Random.Next(0, professionsSize);
					for (int j = 0; j < professionsPerCategory; ++j)
					{
						Profession profession = new Profession()
						{
							Name = professionNames[Random.Next(0, professionsSize)],
							IdProfCategory = profCategories[Random.Next(0, profCategoriesSize)].Id
						};
						professions.Add(profession);
					}
					Storage.ProfessionDao.insertEntities(professions);
				}
				Logger.Info("Professions Table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}			
		}
	}
}
