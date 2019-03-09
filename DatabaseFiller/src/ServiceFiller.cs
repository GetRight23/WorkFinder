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
			List<string> services = new List<string>();

			try
			{
				List<Service> serviceList = new List<Service>();
				List<Profession> professions = Storage.ProfesionDao.selectEntities();

				fileLoader.load(@".\res\services.txt");
				services.AddRange(fileLoader.Entities);


				for (int i = 0; i < services.Count; i++)
				{
					Service service = new Service()
					{
						Price = Random.Next(1000, 5000),
						Name = services[Random.Next(0, services.Count)],
						IdProffesion = professions[Random.Next(0, professions.Count)].Id
					};
					serviceList.Add(service);
				}
				Storage.ServiceDao.insertEntities(serviceList);
				m_logger.Trace("Services Table filled");
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.Message);
			}			
		}
	}
}
