using System;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using DatabaseDao;
using System.Collections.Generic;
using JSONConvertor;

namespace TestApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Storage storage = PostgreStorage.getInstance();
			//City city = new City() { Name = "Kiev" };
			//storage.CityDao.insertEntity(city);
			//storage.WorkerDao.insertEntity(new Worker
			//{
			//	PhoneNumber = 124124,
			//	Info = "asfasf",
			//	IdAddress = 11,
			//	Name = "Иван",
			//	LastName = "Иванов"
			//});
			//City city = storage.CityDao.selectEntityById(1);
			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
