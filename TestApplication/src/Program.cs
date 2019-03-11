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
			Storage storage = new PostgreStorage(Configuration.TestConnection);
			storage.createTables();

			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
