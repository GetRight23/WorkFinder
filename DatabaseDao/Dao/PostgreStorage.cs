using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Npgsql;
using System.Data.Common;

namespace DatabaseDao
{
    public class PostgreStorage : Storage 
    {        
        private static PostgreStorage instance = null;

        private PostgreStorage() 
            : base(new NpgsqlConnection(@"Host=138.197.176.34;Port=5432;Database=test2;Username=oleg;Password=oleg")) {}

        public static PostgreStorage getInstance()
        {
            if (instance == null)
                instance = new PostgreStorage();
            return instance;
        }
        public override void createCityTable(DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText =
                "create table if not exists city ( " +
                "id serial primary key, " +
                "name character varying(30) not null" +
                ")";
            command.ExecuteNonQuery();
        }
        public override void createCityDistrictsTable(DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText =
                "create table if not exists city_districts (" +
                "id serial primary key, " +
                "name character varying(30) not null" +
                ")";
            command.ExecuteNonQuery();
        }
        public override void createAddressTable(DbConnection connection)
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
        }
        public override void createWorkerTable(DbConnection connection)
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
        }
        public override void createFeedbackTable(DbConnection connection)
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
        }
        public override void createOrderListTable(DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText =
                "create table if not exists orderList (" +
                "id serial primary key, " +
                "id_worker integer references worker(id) not null" +
                ")";
            command.ExecuteNonQuery();
        }
        public override void createOrderTable(DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText =
                "create table if not exists orderList (" +
                "id serial primary key, " +
                "info character varying(500) not null, " +
                "id_order_list integer references OrdersList(id) not null" +
                ")";
            command.ExecuteNonQuery();
        }
        public override void createProfCategoryTable(DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText =
                "create table if not exists prof_category (" +
                "id serial primary key, " +
                "name character varying(45) not null" +
                ")";
            command.ExecuteNonQuery();
        }
        public override void createProfessionTable(DbConnection connection)
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
        }
        public override void createServiceTable(DbConnection connection)
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
        }
        public override void createOrderToService(DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText =
                "create table if not exists order_to_service (" +
                "id serial primary key, " +
                "id_service integer references service(id) not null, " +
                "id_order integer references service(id) not null" +
                ")";
            command.ExecuteNonQuery();
        }
    }
}
