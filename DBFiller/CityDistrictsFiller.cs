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
			FileLoader loader = new FileLoader();
			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\city_districts.txt");

			List<string> cityDistricts = new List<string>();
			cityDistricts.AddRange(loader.entities);

			Random rand = new Random();

			List<CityDistricts> listCityDistricts = new List<CityDistricts>();
			List<City> listCity = Storage.CityDao.selectEntities();

			for (int i = 0; i < cityDistricts.Count; i++)
			{
				CityDistricts district = new CityDistricts()
				{
					Name = cityDistricts[rand.Next(0, cityDistricts.Count)],
					IdCity = listCity[rand.Next(0, listCity.Count)].Id
				};
				listCityDistricts.Add(district);
			}
			Storage.CityDistrictsDao.insertEntities(listCityDistricts);
		}
	}
}
