using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;

namespace DatabaseFiller
{
	class ProfesssionCategoryFiller : DBFiller
	{
		public ProfesssionCategoryFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			try
			{
				List<ProfessionCategory> ProfessionCategories = new List<ProfessionCategory>();

				List<string> professionCategories = FileLoader.load(@".\res\profession_categories.txt");

				for (int i = 0; i < professionCategories.Count; i++)
				{
					ProfessionCategory profCategory = new ProfessionCategory()
					{
						Name = professionCategories[i]
					};
					ProfessionCategories.Add(profCategory);
				}
				Storage.ProfessionCategoryDao.insertEntities(ProfessionCategories);
				Logger.Info("Profession category table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				Logger.Error("Professsion category filler filling failed");
			}			
		}
	}
}
