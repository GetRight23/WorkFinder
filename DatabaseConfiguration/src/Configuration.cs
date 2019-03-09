﻿using System;
using System.Configuration;

namespace DatabaseConfiguration
{
	public static class Configuration
	{
		public static string GetConnectionString()
		{
			string connectionString = null;
			try
			{
				var appSettings = ConfigurationManager.AppSettings;

				if (appSettings.Count == 0)
				{
					Console.WriteLine("AppSettings is empty.");
				}
				else
				{
                    connectionString = appSettings["ConnectionString"];
				}
			}
			catch (ConfigurationErrorsException)
			{
				Console.WriteLine("Error reading app settings");
			}
			return connectionString;
		}
	}
}
