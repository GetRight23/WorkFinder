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

			Storage storage = new PostgreStorage();
			List<int> ids_lst = new List<int>() { 1, 2, 3, 4, 5 };
			List<City> cities = storage.CityDao.selectEntitiesByIds(ids_lst);
			foreach (var city in cities)
			{
				Console.WriteLine($"{city.Id} - {city.Name}");
			}
			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
