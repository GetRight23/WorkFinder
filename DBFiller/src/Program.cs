using DatabaseDao;
using System;
using DatabaseConfiguration;

namespace DBFiller
{
	class Program
	{
		static void Main(string[] args)
		{
			Storage storage = new PostgreStorage(Configuration.DefaultConnection);
			storage.createTables();

			AddressFiller addressFiller = new AddressFiller(storage);
			addressFiller.fillEntities();

			Console.ReadKey();
		}
	}
}
