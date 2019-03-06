using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFiller
{
	class ServiceFiller : DBFiller
	{
		public ServiceFiller(Storage storage) : base(storage) {}
		public override void fillEntities()
		{
			FileLoader loader = new FileLoader();
			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\services.txt");

			List<string> services = new List<string>();
			services.AddRange(loader.entities);

			Random rand = new Random();

			List<Service> serviceList = new List<Service>();

			List<Profession> professions = Storage.ProfesionDao.selectEntities();

			for (int i = 0; i < services.Count; i++)
			{
				Service service = new Service()
				{
					Price = rand.Next(1000, 5000),
					Name = services[rand.Next(0, services.Count)],
					IdProffesion = professions[rand.Next(0, professions.Count)].Id
				};
				serviceList.Add(service);
			}
			Storage.ServiceDao.insertEntities(serviceList);
		}
	}
}
