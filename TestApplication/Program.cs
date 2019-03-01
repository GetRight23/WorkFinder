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
            Storage storage = Storage.getInstance();


            var temp = storage.CityDao.selectEntities();
            JsonConvertor jsonConvertor = new JsonConvertor();
            foreach (var item in temp)
            {
                Console.WriteLine(jsonConvertor.toJson(item).ToString());
            }


            Console.WriteLine("Done");
			Console.ReadKey();           
        }
	}
}
