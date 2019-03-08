using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFiller
{
	class CityDistrictsFiller : DBFiller
	{
		public CityDistrictsFiller(Storage storage) : base(storage){}
		public override void fillEntities()
		{
			List<string> cityDistricts = new List<string>();

			List<CityDistricts> listCityDistricts = new List<CityDistricts>();
			List<City> listCity = Storage.CityDao.selectEntities();

			fileLoader.load(@".\res\city_districts.txt");
			
			cityDistricts.AddRange(fileLoader.entities);
		
			for (int i = 0; i < cityDistricts.Count; i++)
			{
				CityDistricts district = new CityDistricts()
				{
					Name = cityDistricts[Random.Next(0, cityDistricts.Count)],
					IdCity = listCity[Random.Next(0, listCity.Count)].Id
				};
				listCityDistricts.Add(district);
			}
			Storage.CityDistrictsDao.insertEntities(listCityDistricts);
		}
	}
}
