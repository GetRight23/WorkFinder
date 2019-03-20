using System.Collections.Generic;
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
		private Storage storage;
		private JsonConvertorEngine jsonConvertor;

		public CityDistrictsController(Storage storage, JsonConvertorEngine jsonConvertor)
		{
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
			return wrapper.getJson(jArray);
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


		// GET: api/v1/CityDistricts/5/City - select city by district id
		[HttpGet("{id}/City")]
		public string GetCityByDistrictId(int id)
		{
			JsonWrapper wrapper = new JsonWrapper();

			if (id < 0)
			{
				wrapper.appendError("Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			CityDistricts district = this.storage.CityDistrictsDao.selectEntityById(id);

			if (district == null)
			{
				wrapper.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			City city = storage.CityDao.selectEntityById(district.IdCity);

			if (city == null)
			{
				wrapper.appendError($"Can not find city id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			JObject jObject = new JObject();
			jObject = jsonConvertor.CityJsonConvertor.toJson(city);

			return wrapper.getJson(jObject);
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

			if(id == 0)
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

			CityDistricts newCityDistrict = jsonConvertor.CityDistrictJsonConvertor.fromJson(value);

			if (newCityDistrict == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			newCityDistrict.Id = id;

			bool result = storage.CityDistrictsDao.updateEntity(newCityDistrict);
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
