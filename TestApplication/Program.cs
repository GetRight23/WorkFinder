using System;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using DatabaseDao;
using System.Collections.Generic;
using JSONConvertor;
using Newtonsoft.Json.Linq;

namespace TestApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;

			List<City> cities = new List<City>();
			cities.Add(new City { Name = "xyi" });
			cities.Add(new City { Name = "pezda" });

			Storage storage = new PostgreStorage();
			storage.CityDao.insertEntities(cities);

			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
