using System;
using System.Text;
using DatabaseDao;
using DatabaseConfiguration;
using Models;
using DatabaseCache;

namespace TestApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Storage storage = new PostgreStorage(Configuration.TestConnection);
			storage.createTables();

			int cityId = storage.CityDao.insertEntity(
				new City() { Name="Kiev" }
			);
			int cityDistrictId = storage.CityDistrictsDao.insertEntity(
				new CityDistricts() { Name = "Obolon", IdCity = cityId }
			);
			int addressId = storage.AddressDao.insertEntity(
				new Address() { IdCity = cityId, IdCityDistrict = cityDistrictId, StreetName = "Sribnokilska", ApptNum = "123" }
			);

			int profId = storage.ProfCategoryDao.insertEntity(new ProfCategory() { Name = "it" });

			storage.ProfessionDao.insertEntity(
				new Profession() { Name = "Programmer", IdProfCategory = profId }
			);

			storage.WorkerDao.insertEntity(
				new Worker() { Name = "Oleg", LastName="Shulzhenko", PhoneNumber = "+380502889767", Info = "Test", IdAddress = addressId }
			);

			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
