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

			ApplicationContext ctx = new ApplicationContext();
			AddressDao addressDao = new AddressDao(ctx);


			Address address = new Address("Xyevina", "", 2, 1);
			addressDao.insertAddress(address);


			Console.ReadKey();
			ctx.Database.CloseConnection();
		}
	}
}
