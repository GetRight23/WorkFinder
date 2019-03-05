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

			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
