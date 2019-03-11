using DatabaseDao;
using System;
using DatabaseConfiguration;

namespace DBFiller
{
	class Program
	{
		static void Main(string[] args)
		{
			Storage storage = new PostgreStorage(Configuration.TestConnection);
			storage.createTables();

			AddressFiller addressFiller = new AddressFiller(storage);
			CityDistrictsFiller cityDistrictsFiller = new CityDistrictsFiller(storage);
			CityFiller cityFiller = new CityFiller(storage);

			cityFiller.fillEntities();
			cityDistrictsFiller.fillEntities();
			addressFiller.fillEntities();
		}
	}
}
