using DatabaseDao;
using Microsoft.AspNetCore.Mvc;
using JsonConvertor;
using Models;
using Newtonsoft.Json.Linq;
using DatabaseCache;
using System.Net;

namespace WorkFinderAPI.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class CityController : ControllerBase
	{
		private Storage storage;
		private JsonConvertorEngine jsonConvertor;
        private CityCache cache;

        public CityController(Storage storage, JsonConvertorEngine jsonConvertor)
		{
			this.storage = storage;
			this.jsonConvertor = jsonConvertor;
            cache = new CityCache(storage, storage.CityDao);
        }

		// GET api/v1/city
		[HttpGet]
		public string Get()
		{
			cache.updateCache();
			return cache.CachedJson;
		}

		//TODO: сделать проверки во всех методах
		// GET api/v1/city/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			cache.updateCache();
			if (!cache.citiesCache.ContainsKey(id))
			{
				JObject jObject = new JObject();
				jObject["Value"] = null;
				jObject["Error message"] = $"Can not find id {id}";
				return jObject.ToString();
			}
			return cache.citiesCache[id];
		}

		// POST api/v1/city
		[HttpPost]
		public string Post([FromBody] JObject value)
		{
			JsonWrapper wrapper = new JsonWrapper();

			if(value == null)
			{
				wrapper.appendError("JSON parametr is null");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			City city = jsonConvertor.CityJsonConvertor.fromJson(value);

			if(city == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			int id = storage.CityDao.insertEntity(city);

			if(id == 0)
			{
				wrapper.appendError($"Can not insert City with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

		// POST api/v1/city
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

			City newCity = jsonConvertor.CityJsonConvertor.fromJson(value);

			if (newCity == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			newCity.Id = id;

			bool result = storage.CityDao.updateEntity(newCity);

			if(result == false)
			{
				wrapper.appendError($"Can not update City with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

		// DELETE api/values/5 - delete by id
		[HttpDelete("{id}")]
		public string Delete(int id)
		{
			JsonWrapper wrapper = new JsonWrapper();

			if (id < 0)
			{
				wrapper.appendError($"Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			bool result = storage.CityDao.deleteEntityById(id);

			if (result == false)
			{
				wrapper.appendError($"Can not delete City with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}
	}
}
