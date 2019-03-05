using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ConfigurationClass
{
	public static class ConfigurationClass
	{
		public static string GetConnectionString()
		{
			string connStr = null;
			try
			{
				var appSettings = ConfigurationManager.AppSettings;

				if (appSettings.Count == 0)
				{
					Console.WriteLine("AppSettings is empty.");
				}
				else
				{
					connStr = appSettings["ConnectionString"];
				}
			}
			catch (ConfigurationErrorsException)
			{
				Console.WriteLine("Error reading app settings");
			}
			return connStr;
		}
	}
}
