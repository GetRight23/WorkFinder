using DatabaseDao;
using Models;
using NLog;
using System;
using System.Collections.Generic;

namespace DatabaseFiller
{
	class CityFiller : DBFiller
	{
		public CityFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			try
			{
				List<string> citiesNames = FileLoader.load(@".\res\cities.txt");

				List<City> cities = new List<City>();
				for (int i = 0; i < citiesNames.Count; i++)
				{
					City city = new City()
					{
						Name = citiesNames[i]
					};
					cities.Add(city);
				}
				Storage.CityDao.insertEntities(cities);
				Logger.Info("City table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.InnerException.Message);
				Logger.Error("City filler filling failed");
			}			
		}
	}
}
