using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFiller
{
	class ProfCategoryFiller : DBFiller
	{
		public ProfCategoryFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			List<string> prof_categories = new List<string>();

			List<ProfCategory> listProfCategory = new List<ProfCategory>();

			fileLoader.load(@".\res\prof_categories.txt");		
			prof_categories.AddRange(fileLoader.entities);

			for (int i = 0; i < prof_categories.Count; i++)
			{
				ProfCategory profCategory = new ProfCategory()
				{
					Name = prof_categories[i]
				};
				listProfCategory.Add(profCategory);
			}
			Storage.ProfCategoryDao.insertEntities(listProfCategory);
		}
	}
}
