﻿using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFiller
{
	public class WorkerFiller : DBFiller
	{
		public WorkerFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			const int size = 10;
			List<string> names = new List<string>();
			List<string> lastNames = new List<string>();

			try
			{
				List<Address> addresses = Storage.AddressDao.selectEntities();
				List<Worker> workers = new List<Worker>();

				fileLoader.load(@".\res\russian_names.txt");
				names.AddRange(fileLoader.Entities);

				fileLoader.load(@".\res\russian_surnames.txt");
				lastNames.AddRange(fileLoader.Entities);

				for (int i = 0; i < size; i++)
				{
					int firstPart = Random.Next(10, 99);
					int secondPart = Random.Next(100, 999);
					int thirdPart = Random.Next(1000, 9999);
					Worker worker = new Worker()
					{
						PhoneNumber = $"+380{firstPart}{secondPart}{thirdPart}",
						Info = "test",
						IdAddress = addresses[Random.Next(0, addresses.Count)].Id,
						Name = names[Random.Next(0, names.Count)],
						LastName = lastNames[Random.Next(0, lastNames.Count)]
					};
					workers.Add(worker);
				}
				Storage.WorkerDao.insertEntities(workers);
				m_logger.Trace("Worker Table filled");
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.Message);
			}		
		}
	}
}
