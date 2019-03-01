using System;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using DatabaseDao;
using System.Collections.Generic;

namespace TestApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
            Storage storage = Storage.getInstance();

            var temp = storage.CityDao.selectEntities();
            foreach (var item in temp)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("Done");
			Console.ReadKey();           
        }
	}
}
