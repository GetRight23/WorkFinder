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

			DBFiller dBFiller = new DBFiller(storage);
			dBFiller.

			Console.WriteLine("Done");
			Console.ReadKey();           
        }
	}
}
