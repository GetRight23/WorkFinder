using DatabaseDao;
using System;
using DatabaseConfiguration;

namespace DBFiller
{
	class Program
	{
		static void Main(string[] args)
		{
			Storage storage = new PostgreStorage(Configuration.TestConnection);
			storage.createTables();

			CityFiller cityFiller = new CityFiller(storage);
			CityDistrictsFiller cityDistrictsFiller = new CityDistrictsFiller(storage);
			AddressFiller addressFiller = new AddressFiller(storage);
			UserFiller userFiller = new UserFiller(storage);
			WorkerFiller workerFiller = new WorkerFiller(storage);
			FeedbackFiller feedbackFiller = new FeedbackFiller(storage);
			OrdersListFiller ordersListFiller = new OrdersListFiller(storage);
			OrdersFiller ordersFiller = new OrdersFiller(storage);
			ProfesssionCategoryFiller professsionCategoryFiller = new ProfesssionCategoryFiller(storage);
			ProfessionFiller professionFiller = new ProfessionFiller(storage);

			cityFiller.fillEntities();
			cityDistrictsFiller.fillEntities();
			addressFiller.fillEntities();
			userFiller.fillEntities();
			workerFiller.fillEntities();
			feedbackFiller.fillEntities();
			ordersListFiller.fillEntities();
			ordersFiller.fillEntities();
			professsionCategoryFiller.fillEntities();
			professionFiller.fillEntities();
		}
	}
}
