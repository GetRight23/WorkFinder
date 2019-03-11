using DatabaseDao;
using Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBFiller
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

				int streetNamesSize = streetNames.Count;
				int citiesSize = cities.Count;
				List<Address> addresses = new List<Address>();
				for (int i = 0; i < citiesSize; i++)
				{
					List<CityDistricts> cityDistricts = cities[i].CityDistricts.ToList();
					int disrictsSize = cityDistricts.Count;
					for (int j = 0; j < disrictsSize; ++j)
					{
						int stretsPerDirstrict = Random.Next(1, streetNamesSize / 4);
						for (int k = 0; k < stretsPerDirstrict; k++)
						{
							Address address = new Address()
							{
								StreetName = streetNames[Random.Next(0, streetNamesSize)],
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
				Logger.Info("Address Table filled");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}			
		}
	}
}
