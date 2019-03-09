using DatabaseDao;
using Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DBFiller
{
	class CityFiller : DBFiller
	{
		public CityFiller(Storage storage) : base(storage) {}
		public override void fillEntities()
		{
			List<string> cityList = new List<string>();

			List<City> cities = new List<City>();
			try
			{
				fileLoader.load(@".\res\cities.txt");
				cityList.AddRange(fileLoader.Entities);

				for (int i = 0; i < cityList.Count; i++)
				{
					City city = new City()
					{
						Name = cityList[i]
					};
					cities.Add(city);
				}
				Storage.CityDao.insertEntities(cities);
				m_logger.Trace("City Table filled");
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.Message);
			}			
		}
	}
}
