using DatabaseDao;
using Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DBFiller
{
	class CityDistrictsFiller : DBFiller
	{
		public CityDistrictsFiller(Storage storage) : base(storage) {}
		public override void fillEntities()
		{
			try
			{
				List<City> cities = Storage.CityDao.selectEntities();
			
				List<CityDistricts> cityDistricts = new List<CityDistricts>();
				List<string> cityDistrictsNames = FileLoader.load(@".\res\city_districts.txt");

				int citiesSize = cities.Count;
				int cityDistrictNamesSize = cityDistrictsNames.Count;
				for (int i = 0; i < citiesSize; i++)
				{
					int districtsPerCity = Random.Next(1, cityDistrictNamesSize);
					for (int j = 0; j < districtsPerCity; j++)
					{
						CityDistricts district = new CityDistricts()
						{
							Name = cityDistrictsNames[Random.Next(0, cityDistrictNamesSize)],
							IdCity = cities[i].Id
						};
						cityDistricts.Add(district);
					}
					Storage.CityDistrictsDao.insertEntities(cityDistricts);
					cityDistricts.Clear();
				}
				Logger.Info("City Districts Table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}		
		}
	}
}
