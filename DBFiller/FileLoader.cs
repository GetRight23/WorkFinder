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
		public FileLoader() {}

		public List<string> load(string filePath)
		{
			List<string> entities = new List<string>();
			string line = null;
			try
			{
				using (StreamReader fileSurnames = new StreamReader(filePath))
				{
					while ((line = fileSurnames.ReadLine()) != null)
					{
						entities.Add(line);
					}
				}

				return filter(entities);
			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine(ex);
			}
			return null;
		}

		private List<string> filter(List<string> entities)
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
