using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseDao;
using Microsoft.AspNetCore.Mvc;
using DatabaseConfiguration;
using JSONConvertor;
using Models;
using Newtonsoft.Json.Linq;
using DatabaseCache;
using NLog;
using System.Net;

namespace WorkFinderAPI.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class CityController : ControllerBase
	{
		private DBContext m_context;
		private Storage m_storage;
		private CityCache m_cache = null;
		private JsonConvertor m_jsonConvertor = null;

		public CityController(DBContext dBContext, Storage storage)
		{
			m_context = dBContext;
			m_storage = storage;
			m_cache = new CityCache(storage, storage.CityDao);
			m_jsonConvertor = new JsonConvertor();
		}

		// GET api/v1/city
		[HttpGet]
		public string Get()
		{
			m_cache.updateCache();
			return m_cache.CachedJson;
		}

		//TODO: сделать проверки во всех методах
		// GET api/v1/city/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			m_cache.updateCache();
			if (!m_cache.citiesCache.ContainsKey(id))
			{
				JObject jObject = new JObject();
				jObject["Value"] = null;
				jObject["Error message"] = $"Can not find id {id}";
				return jObject.ToString();
			}
			return m_cache.citiesCache[id];
		}

		// POST api/v1/city
		[HttpPost]
		public string Post([FromBody] JObject value)
		{
			JsonHandler handler = new JsonHandler();

			if(value == null)
			{
				handler.appendError("JSON parametr is null");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			City city = m_jsonConvertor.fromJsonToCity(value);

			if(city == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			int id = m_storage.CityDao.insertEntity(city);

			if(id < 0)
			{
				handler.appendError($"Can not insert City with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

		// POST api/v1/city
		[HttpPost("{id}")]
		public string Post(int id, [FromBody] JObject value)
		{
			JsonHandler handler = new JsonHandler();

			if (id < 0)
			{
				handler.appendError($"Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			if (value == null)
			{
				handler.appendError($"JSON parametr is null");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			City city = m_storage.CityDao.selectEntityById(id);

			if (city == null)
			{
				handler.appendError($"Can not find City with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			City newCity = m_jsonConvertor.fromJsonToCity(value);

			if (newCity == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			city.Name = newCity.Name;

			bool result = m_storage.CityDao.updateEntity(city);

			if(result == false)
			{
				handler.appendError($"Can not update City with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

		// DELETE api/values/5 - delete by id
		[HttpDelete("{id}")]
		public string Delete(int id)
		{
			JsonHandler handler = new JsonHandler();

			if (id < 0)
			{
				handler.appendError($"Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			bool result = m_storage.CityDao.deleteEntityById(id);

			if (result == false)
			{
				handler.appendError($"Can not delete City with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}
	}
}
