using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseFiller
{
	class ServiceFiller : DBFiller
	{
		public ServiceFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			try
			{
				List<ProfessionCategory> professionCategories = Storage.ProfessionCategoryDao.selectEntities();

				List<string> servicesNames = FileLoader.load(@".\res\services.txt");

				List<Service> services = new List<Service>();
				for (int i = 0; i < professionCategories.Count; i++)
				{
					List<Profession> proffesions = professionCategories[i].Profession.ToList();
					for (int j = 0; j < proffesions.Count; j++)
					{
						int servicesPerProffesion = Random.Next(1, servicesNames.Count);
						for (int k = 0; k < servicesPerProffesion; k++)
						{
							Service service = new Service()
							{
								Price = Random.Next(1000, 5000),
								Name = servicesNames[Random.Next(0, servicesNames.Count)],
								IdProfession = proffesions[j].Id
							};
							services.Add(service);
						}	
					}
					Storage.ServiceDao.insertEntities(services);
					services.Clear();
				}
				Storage.ServiceDao.insertEntities(services);
				Logger.Info("Services table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}			
		}
	}
}
