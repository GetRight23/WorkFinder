using System;
using System.Text;
using DatabaseDao;
using DatabaseConfiguration;

namespace TestApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Storage storage = new PostgreStorage(Configuration.DefaultConnection);
			storage.createTables();

			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
