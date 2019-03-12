using DatabaseDao;
using Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseFiller
{
	class AddressFiller : DBFiller
	{
		public AddressFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			try
			{
				List<City> cities = Storage.CityDao.selectEntities();

				List<string> streetNames = FileLoader.load(@".\res\street_names.txt");

				List<Address> addresses = new List<Address>();
				for (int i = 0; i < cities.Count; i++)
				{
					List<CityDistricts> cityDistricts = cities[i].CityDistricts.ToList();
					for (int j = 0; j < cityDistricts.Count; j++)
					{
						int stretsPerDirstrict = Random.Next(1, streetNames.Count / 4);
						for (int k = 0; k < stretsPerDirstrict; k++)
						{
							Address address = new Address()
							{
								StreetName = streetNames[Random.Next(0, streetNames.Count)],
								ApptNum = Random.Next(0, 100).ToString(),
								IdCityDistrict = cityDistricts[j].Id,
								IdCity = cities[i].Id
							};
							addresses.Add(address);
						}
					}
					Storage.AddressDao.insertEntities(addresses);
					addresses.Clear();
				}
				Logger.Info("Address table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.InnerException.Message);
				Logger.Error("AddressFiller filling failed");
			}			
		}
	}
}
