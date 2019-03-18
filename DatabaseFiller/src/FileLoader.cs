using NLog;
using System.Collections.Generic;
using System.IO;

namespace DatabaseFiller
{
	class FileLoader
	{
		protected Logger Logger { get; private set; }

		public FileLoader()
		{
			Logger = LogManager.GetCurrentClassLogger();
		}

		public List<string> load(string filePath)
		{
			List<string> entities = new List<string>();
			string line = null;
			try
			{
				using (StreamReader file = new StreamReader(filePath))
				{
					while ((line = file.ReadLine()) != null)
					{
						entities.Add(line);
					}
				}
				return entities;
			}
			catch (FileNotFoundException ex)
			{
				Logger.Error(ex.Message);
				Logger.Error("Cannot open file");
			}
			return null;
		}
	}
}
