using DatabaseDao;
using Models;
using NLog;
using System;
using System.Collections.Generic;

namespace DatabaseFiller
{
	class CityDistrictsFiller : DBFiller
	{
		public CityDistrictsFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			try
			{
				List<City> cities = Storage.CityDao.selectEntities();

				List<string> cityDistrictsNames = FileLoader.load(@".\res\city_districts.txt");

				List<CityDistricts> cityDistricts = new List<CityDistricts>();
				for (int i = 0; i < cities.Count; i++)
				{
					int districtsPerCity = Random.Next(1, cityDistrictsNames.Count);
					for (int j = 0; j < districtsPerCity; j++)
					{
						CityDistricts district = new CityDistricts()
						{
							Name = cityDistrictsNames[Random.Next(0, cityDistrictsNames.Count)],
							IdCity = cities[i].Id
						};
						cityDistricts.Add(district);
					}
					Storage.CityDistrictsDao.insertEntities(cityDistricts);
					cityDistricts.Clear();
				}
				Logger.Info("City districts table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.InnerException.Message);
				Logger.Error("City districts filler filling failed");
			}		
		}
	}
}
