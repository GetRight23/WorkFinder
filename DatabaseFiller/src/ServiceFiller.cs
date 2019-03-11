using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;

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

				services = FileLoader.load(@".\res\services.txt");

				for (int i = 0; i < services.Count; i++)
				{
					Service service = new Service()
					{
						Price = Random.Next(1000, 5000),
						Name = services[Random.Next(0, services.Count)]
					};
					serviceList.Add(service);
				}
				Storage.ServiceDao.insertEntities(serviceList);
				Logger.Info("Services Table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}			
		}
	}
}
