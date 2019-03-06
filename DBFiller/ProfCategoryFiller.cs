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
			FileLoader loader = new FileLoader();
			loader.filter(loader.entities);

			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\prof_categories.txt");
			List<string> prof_categories = new List<string>();
			prof_categories.AddRange(loader.entities);

			Random rand = new Random();
			List<ProfCategory> listProfCategory = new List<ProfCategory>();

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
