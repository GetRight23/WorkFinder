using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DatabaseConfiguration
{
	public class Configuration
	{
		public static IConfiguration AppConfiguration { get; private set; }
		public static string DefaultConnection { get; private set; }
		public static string TestConnection { get; private set; }

		static Configuration()
		{
			var builder = new ConfigurationBuilder();
			builder.AddJsonFile("conf.json");
			AppConfiguration = builder.Build();

			DefaultConnection = "Host=" + AppConfiguration["Host"] + ";";
			DefaultConnection += "Port=" + AppConfiguration["Port"] + ";";
			DefaultConnection += "Database=" + AppConfiguration["DefaultDatabase"] + ";";
			DefaultConnection += "Username=" + AppConfiguration["Username"] + ";";
			DefaultConnection += "Password=" + AppConfiguration["Password"];

			TestConnection = "Host=" + AppConfiguration["Host"] + ";";
			TestConnection += "Port=" + AppConfiguration["Port"] + ";";
			TestConnection += "Database=" + AppConfiguration["TestDatabase"] + ";";
			TestConnection += "Username=" + AppConfiguration["Username"] + ";";
			TestConnection += "Password=" + AppConfiguration["Password"];
		}
	}
}
