using DatabaseDao;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFiller
{
	class AddressFiller : DBFiller
	{
		public AddressFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			string apptNum = null;
			FileLoader loader = new FileLoader();
			List<string> listStreetNames = new List<string>();
			loader.load(@"E:\Projects\WorkFinder\WorkFinder\DBFiller\res\street_names.txt");
			listStreetNames.AddRange(loader.entities);
			
			Random rand = new Random();

			List<City> listCity = Storage.CityDao.selectEntities();
			List<CityDistricts> listCityDistricts = Storage.CityDistrictsDao.selectEntities();

			List<Address> addresses = new List<Address>();

			for (int i = 0; i < listCity.Count; i++)
			{
				apptNum = rand.Next(0, 100).ToString();
				Address address = new Address()
				{
					StreetName = listStreetNames[rand.Next(0, listStreetNames.Count)],
					ApptNum = apptNum,
					IdCityDistrict = listCityDistricts[rand.Next(0, listCityDistricts.Count)].Id,
					IdCity = listCity[rand.Next(0, listCity.Count)].Id
				};
				addresses.Add(address);
			}
			Storage.AddressDao.insertEntities(addresses);
		}
	}
}
