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
			FileLoader loader = new FileLoader();
			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\cities.txt");

			List<string> cityList = new List<string>();
			cityList.AddRange(loader.entities);

			Random rand = new Random();

			List<City> cities = new List<City>();

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
