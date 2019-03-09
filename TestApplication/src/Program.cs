using System;
using System.Text;
using DatabaseDao;
using DatabaseConfiguration;
using Models;
using DatabaseCache;

namespace TestApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Storage storage = new PostgreStorage(Configuration.DefaultConnection);
			storage.createTables();


			CacheDao<City> cacheDao = new CacheDao<City>(storage, storage.CityDao);

			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
