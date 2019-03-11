﻿using System;
using System.Collections.Generic;
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
				List<ProfessionCategory> profCategories = Storage.ProfCategoryDao.selectEntities();

				List<string> professionNames = FileLoader.load(@".\res\professions.txt");

				List<Profession> professions = new List<Profession>();
				for (int i = 0; i < profCategories.Count; i++)
				{
					int professionsPerCategory = Random.Next(1, professionNames.Count);
					for (int j = 0; j < professionsPerCategory; j++)
					{
						Profession profession = new Profession()
						{
							Name = professionNames[Random.Next(0, professionNames.Count)],
							IdProfCategory = profCategories[i].Id
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
