using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Npgsql;
using System.Data.Common;
using NLog;

namespace DatabaseDao
{
	public class PostgreStorage : Storage
	{
		private static PostgreStorage instance = null;
		private Logger logger = LogManager.GetCurrentClassLogger();

		private PostgreStorage()
			: base(new NpgsqlConnection(@"Host=138.197.176.34;Port=5432;Database=test1;Username=oleg;Password=oleg"))
		{
		}

		public static PostgreStorage getInstance()
		{
			if (instance == null)
				instance = new PostgreStorage();
			return instance;
		}
		public override void createCityTable(DbConnection connection)
		{
			try
			{
				var command = connection.CreateCommand();
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
		public override void createCityDistrictsTable(DbConnection connection)
		{
			try
			{
				var command = connection.CreateCommand();
				command.CommandText =
					"create table if not exists city_districts (" +
					"id serial primary key, " +
					"name character varying(30) not null" +
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
		public override void createAddressTable(DbConnection connection)
		{
			try
			{
				var command = connection.CreateCommand();
				command.CommandText =
					"create table if not exists address (" +
					"id serial primary key, " +
					"street_name character varying(45) not null, " +
					"appt_num character varying(5) not null, " +
					"id_city_district integer references city_districts(id) not null, " +
					"id_city integer references city(id) not null" +
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
		public override void createWorkerTable(DbConnection connection)
		{
			try
			{
				var command = connection.CreateCommand();
				command.CommandText =
					"create table if not exists worker (" +
					"id serial primary key, " +
					"phone_number float not null, " +
					"address integer references city_districts(id) not null, " +
					"info character varying(500) not null, " +
					"id_address integer references address(id) not null" +
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
		public override void createFeedbackTable(DbConnection connection)
		{
			try
			{
				var command = connection.CreateCommand();
				command.CommandText =
					"create table if not exists feedback (" +
					"id serial primary key, " +
					"name character varying(30) not null, " +
					"patronymic character varying(30) not null, " +
					"grade_value integer not null, " +
					"date date not null, " +
					"text character varying(500), " +
					"id_worker integer references worker(id) not null" +
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
		public override void createOrdersListTable(DbConnection connection)
		{
			try
			{
				var command = connection.CreateCommand();
				command.CommandText =
					"create table if not exists orderslist (" +
					"id serial primary key, " +
					"id_worker integer references worker(id) not null" +
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
		public override void createOrderTable(DbConnection connection)
		{
			try
			{
				var command = connection.CreateCommand();
				command.CommandText =
					"create table if not exists order_table (" +
					"id serial primary key, " +
					"info character varying(500) not null, " +
					"id_order_list integer references orderslist(id) not null" +
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
		public override void createProfCategoryTable(DbConnection connection)
		{
			try
			{
				var command = connection.CreateCommand();
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
		public override void createProfessionTable(DbConnection connection)
		{
			try
			{
				var command = connection.CreateCommand();
				command.CommandText =
					"create table if not exists profession (" +
					"id serial primary key, " +
					"category_name character varying(45) not null, " +
					"name character varying(45) not null, " +
					"id_worker integer references worker(id) not null, " +
					"id_prof_category integer references prof_category(id) not null" +
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
		public override void createServiceTable(DbConnection connection)
		{
			try
			{
				var command = connection.CreateCommand();
				command.CommandText =
					"create table if not exists service (" +
					"id serial primary key, " +
					"price money not null, " +
					"name character varying(100) not null, " +
					"id_proffesion integer references profession(id) not null" +
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
		public override void createOrderToService(DbConnection connection)
		{
			try
			{
				var command = connection.CreateCommand();
				command.CommandText =
					"create table if not exists order_to_service (" +
					"id serial primary key, " +
					"id_service integer references service(id) not null, " +
					"id_order integer references order_table(id) not null" +
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
	}
}

//create table users(
//					id serial primary key,
//					login character varying(256) not null,
//					password character varying(256) not null,
//					id_worker integer references worker(id) unique
//					)

//create table photo(
//				id serial primary key,
//				link character varying(256) not null,
//				id_user integer references users(id)
//				)