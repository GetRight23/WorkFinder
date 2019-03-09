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
		public List<string> Entities { get; set; }
		public FileLoader()
		{
			Entities = new List<string>();
		}

		public virtual List<string> load(string filePath)
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
			return null;
		}

		public List<string> filter(List<string> entities)
		{
			const int maxSurnames = 100;
			char endOfAlpahbet = Convert.ToChar((int)'Я' + 1);
			char currentLetter = 'А';

			Random rand = new Random();
			List<string> result = new List<string>();
			while (Convert.ToChar((int)currentLetter) != endOfAlpahbet)
			{
				IEnumerable<string> filteredStrings = entities.Where(str => str[0] == currentLetter);
				for (int i = 0; i < maxSurnames && i != filteredStrings.Count(); i++)
				{
					int index = rand.Next(0, filteredStrings.Count());
					result.Add(filteredStrings.ElementAt(index));
				}
				currentLetter = Convert.ToChar((int)currentLetter + 1);
			}
			return result;
		}
	}
}
