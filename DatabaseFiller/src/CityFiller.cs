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
			try
			{
				List<string> citiesNames = FileLoader.load(@".\res\cities.txt");
				int cityNamesSize = citiesNames.Count;

				List<City> cities = new List<City>();
				for (int i = 0; i < cityNamesSize; i++)
				{
					City city = new City()
					{
						Name = citiesNames[i]
					};
					cities.Add(city);
				}
				Storage.CityDao.insertEntities(cities);
				Logger.Info("City Table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}			
		}
	}
}
