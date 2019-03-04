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
			Storage storage = PostgreStorage.getInstance();
			JsonConvertor jsonConvertor = new JsonConvertor();
			storage.UserDao.insertEntity(new User { Login = "OlegiDauni", Password = "asfasfasf", IdWorker = 20 });
			storage.PhotoDao.insertEntity(new Photo { IdUser = 1, Link = "fdafgagsdg" });
			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
