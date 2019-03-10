using DatabaseDao;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DBFiller
{
	public class FileLoader
	{
		public List<string> Entities { get; private set; }
		public FileLoader()
		{
			Entities = new List<string>();
		}

		public void load(string filePath)
		{
			Entities.Clear();
			string line = null;
			try
			{
				using (StreamReader file = new StreamReader(filePath))
				{
					while ((line = file.ReadLine()) != null)
					{
						Entities.Add(line);
					}
				}
			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}
