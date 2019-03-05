using System;

namespace ConfigurationClass
{
	class Program
	{
		static void Main(string[] args)
		{
			string str = ConfigurationClass.GetConnectionString();
			Console.WriteLine(str);
			Console.WriteLine("Hello World!");
			Console.ReadKey();
		}
	}
}
