using DatabaseDao;
using Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DBFiller
{
	class AddressFiller : DBFiller
	{
		public AddressFiller(Storage storage) : base(storage) {}

		public override void fillEntities()
		{
			string apptNum = null;
			List<string> listStreetNames = new List<string>();
			try
			{
				List<City> listCity = Storage.CityDao.selectEntities();
				List<CityDistricts> listCityDistricts = Storage.CityDistrictsDao.selectEntities();

				List<Address> addresses = new List<Address>();

				fileLoader.load(@".\res\street_names.txt");
				listStreetNames.AddRange(fileLoader.Entities);

				for (int i = 0; i < listCity.Count; i++)
				{
					apptNum = Random.Next(0, 100).ToString();
					Address address = new Address()
					{
						StreetName = listStreetNames[Random.Next(0, listStreetNames.Count)],
						ApptNum = apptNum,
						IdCityDistrict = listCityDistricts[Random.Next(0, listCityDistricts.Count)].Id,
						IdCity = listCity[Random.Next(0, listCity.Count)].Id
					};
					addresses.Add(address);
				}
				Storage.AddressDao.insertEntities(addresses);
				m_logger.Trace("Address Table filled");
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.Message);
			}			
		}
	}
}
