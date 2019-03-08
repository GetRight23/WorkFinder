using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using DatabaseConfiguration;

namespace DBFiller
{
	class Program
	{
		static void Main(string[] args)
		{
			Storage storage = new PostgreStorage();

			AddressFiller addressFiller = new AddressFiller(storage);
			addressFiller.fillEntities();

			Console.ReadKey();
		}
	}
}
