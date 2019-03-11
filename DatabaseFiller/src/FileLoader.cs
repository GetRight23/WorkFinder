using DatabaseDao;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DBFiller
{
	class FileLoader
	{
		public FileLoader() {}

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
				Console.WriteLine(ex);
			}
			return null;
		}
	}
}
