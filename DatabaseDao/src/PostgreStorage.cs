using System;
using Npgsql;
using NLog;

namespace DatabaseDao
{
	public class PostgreStorage : Storage
	{
		private Logger logger = LogManager.GetCurrentClassLogger();

		public PostgreStorage(string connectionString)
			: base(new NpgsqlConnection(connectionString))
		{ }

		public override void createCityTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists city ( " +
					"id serial primary key, " +
					"name character varying(30) not null" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("City table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create city table");
			}
		}
		public override void createCityDistrictsTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists city_districts (" +
					"id serial primary key, " +
					"name character varying(30) not null," +
					"id_city integer references city(id) on delete cascade not null" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("City_districts table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create city_districts table");
			}
		}
		public override void createAddressTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists address (" +
					"id serial primary key, " +
					"street_name character varying(45) not null, " +
					"appt_num character varying(5) not null, " +
					"id_city_district integer references city_districts(id) on delete cascade not null, " +
					"id_city integer references city(id) on delete cascade not null" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("Address table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create address table");
			}
		}
		public override void createWorkerTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists worker (" +
					"id serial primary key, " +
					"phone_number character varying(15) not null, " +					
					"info character varying(500) not null, " +
					"id_address integer references address(id) on delete cascade not null, " +
					"name character varying(30) not null, " +
					"last_name character varying(30) not null,"+
					"id_user integer references user_table(id) on delete cascade not null unique" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("Worker table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create worker table");
			}
		}
		public override void createFeedbackTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists feedback (" +
					"id serial primary key, " +
					"name character varying(30) not null, " +
					"patronymic character varying(30) not null, " +
					"grade_value integer not null, " +
					"date date not null, " +
					"text character varying(500), " +
					"id_worker integer references worker(id) on delete cascade not null" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("Feedback table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create Feedback table");
			}
		}
		public override void createOrdersListTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists orderslist (" +
					"id serial primary key, " +
					"id_worker integer references worker(id) on delete cascade not null" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("OrderList table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create OrderList table");
			}
		}
		public override void createOrderTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists order_table (" +
					"id serial primary key, " +
					"info character varying(500) not null, " +
					"id_order_list integer references orderslist(id) on delete cascade not null" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("Order table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create Order table");
			}
		}
		public override void createProfCategoryTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists prof_category (" +
					"id serial primary key, " +
					"name character varying(45) not null" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("Prof_category table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create Prof_category table");
			}
		}
		public override void createProfessionTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists profession (" +
					"id serial primary key, " +
					"name character varying(45) not null, " +
					"id_prof_category integer references prof_category(id) on delete cascade not null" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("Profession table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create Profession table");
			}
		}
		public override void createServiceTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists service (" +
					"id serial primary key, " +
					"price money not null, " +
					"name character varying(100) not null, " +
					"id_profession integer references profession(id) on delete cascade not null" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("Service table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create Service table");

			}
		}
		public override void createOrderToService()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists order_to_service (" +
					"id serial primary key, " +
					"id_service integer references service(id) on delete cascade not null, " +
					"id_order integer references order_table(id) on delete cascade not null" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("Order_to_service table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create Order_to_service table");
			}
		}
		public override void createUserTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists user_table (" +
						"id serial primary key, " +
						"login character varying(256) not null, " +
						"password character varying(256) not null "+
					")";
				command.ExecuteNonQuery();
				logger.Trace("User table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create User table");
			}
		}
		public override void createPhotoTable()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists photo (" +
						"id serial primary key, " +
						"data BYTEA, " +
                        "id_user integer references user_table(id) on delete cascade not null unique" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("Photo table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create Photo table");
			}
		}

		public override void createProfessionToWorker()
		{
			try
			{
				var command = Connection.CreateCommand();
				command.CommandText =
					"create table if not exists profession_to_worker (" +
					"id serial primary key, " +
					"id_profession integer references profession(id) on delete cascade not null, " +
					"id_worker integer references worker(id) on delete cascade not null" +
					")";
				command.ExecuteNonQuery();
				logger.Trace("Profession_to_worker table is created");
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error("Cannot create Profession_to_worker table");
			}
		}
	}
}
