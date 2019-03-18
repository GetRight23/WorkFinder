﻿using System.Collections.Generic;
using DatabaseDao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using JsonConvertor;
using Newtonsoft.Json.Linq;
using System.Net;

namespace WorkFinderAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityDistrictsController : ControllerBase
    {
		private DBContext context;
		private Storage storage;
		private JsonConvertorEngine jsonConvertor;

		public CityDistrictsController(DBContext context, Storage storage, JsonConvertorEngine jsonConvertor)
		{
			this.context = context;
			this.storage = storage;
			this.jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/CityDistricts
		[HttpGet]
        public string Get()
        {
			JsonWrapper wrapper = new JsonWrapper();
			List<CityDistricts> cityDistricts = storage.CityDistrictsDao.selectEntities();
			if(cityDistricts == null)
			{
				wrapper.appendError("Can not select City Districts");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			JArray jArray = new JArray();
			foreach (var item in cityDistricts)
			{
				JObject cityDistrict = jsonConvertor.CityDistrictJsonConvertor.toJson(item);
				if(cityDistrict != null)
				{
					jArray.Add(cityDistrict);
				}				
			}
			return jArray.ToString();
		}

		// GET: api/v1/CityDistricts/5
		[HttpGet("{id}")]
        public string Get(int id)
        {
			JsonWrapper wrapper = new JsonWrapper();
			if (id < 0)
			{
				wrapper.appendError($"Id is less then 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;				
				return wrapper.getJson();
			}

			CityDistricts cityDistrict = storage.CityDistrictsDao.selectEntityById(id);
			JObject jObject = new JObject();

			if (cityDistrict == null)
			{
				wrapper.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}
			jObject = jsonConvertor.CityDistrictJsonConvertor.toJson(cityDistrict);

			return jObject.ToString();
        }

		// POST:api/v1/CityDistricts
		[HttpPost]
        public string Post([FromBody] JObject value)
        {
			JsonWrapper wrapper = new JsonWrapper();
			if (value == null)
			{
				wrapper.appendError($"JSON parametr is null");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			CityDistricts newCityDistrict = jsonConvertor.CityDistrictJsonConvertor.fromJson(value);
			if (newCityDistrict == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			int id = storage.CityDistrictsDao.insertEntity(newCityDistrict);
			if(id < 0)
			{
				wrapper.appendError($"Can not insert City District with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
        }

		[HttpPost("{id}")]
		public string Post(int id, [FromBody] JObject value)
		{
			JsonWrapper wrapper = new JsonWrapper();
			if (id < 0)
			{
				wrapper.appendError($"Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			if (value == null)
			{
				wrapper.appendError($"JSON parametr is null");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			CityDistricts cityDistrict = storage.CityDistrictsDao.selectEntityById(id);
			if (cityDistrict == null)
			{
				wrapper.appendError($"Can not find City District with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			CityDistricts newCityDistrict = jsonConvertor.CityDistrictJsonConvertor.fromJson(value);
			if(newCityDistrict == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			cityDistrict.Name = newCityDistrict.Name;

			bool result = storage.CityDistrictsDao.updateEntity(cityDistrict);
			if(result == false)
			{
				wrapper.appendError($"Can not update City District with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

		// DELETE: api/v1/ApiWithActions/5
		[HttpDelete("{id}")]
        public string Delete(int id)
        {
			JsonWrapper wrapper = new JsonWrapper();
			if (id < 0)
			{
				wrapper.appendError($"Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			bool result = storage.CityDistrictsDao.deleteEntityById(id);

			if(result == false)
			{
				wrapper.appendError($"Can not delete City District with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
        }
    }
}
