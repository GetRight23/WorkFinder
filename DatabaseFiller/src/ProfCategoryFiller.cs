using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DBFiller
{
	class ProfCategoryFiller : DBFiller
	{
		public ProfCategoryFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			try
			{
				List<string> prof_categories = new List<string>();
				List<ProfCategory> listProfCategory = new List<ProfCategory>();
				prof_categories = FileLoader.load(@".\res\prof_categories.txt");

				for (int i = 0; i < prof_categories.Count; i++)
				{
					ProfCategory profCategory = new ProfCategory()
					{
						Name = prof_categories[i]
					};
					listProfCategory.Add(profCategory);
				}
				Storage.ProfCategoryDao.insertEntities(listProfCategory);
				Logger.Info("Prof Category Table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}			
		}
	}
}
