using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
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

			fileLoader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\cities.txt");		
			cityList.AddRange(fileLoader.entities);

			for (int i = 0; i < cityList.Count; i++)
			{
				City city = new City()
				{
					Name = cityList[i]
				};
				cities.Add(city);
			}
			Storage.CityDao.insertEntities(cities);
		}
	}
}
