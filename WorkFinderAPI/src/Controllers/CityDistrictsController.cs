using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseDao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using JSONConvertor;
using Newtonsoft.Json.Linq;
using DatabaseCache;
using System.Net;

namespace WorkFinderAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityDistrictsController : ControllerBase
    {
		private DBContext m_context;
		private Storage m_storage;
		private JsonConvertorEngine m_jsonConvertor;

		public CityDistrictsController(DBContext dBContext, Storage storage, JsonConvertorEngine jsonConvertor)
		{
			m_context = dBContext;
			m_storage = storage;
			m_jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/CityDistricts
		[HttpGet]
        public string Get()
        {
			JsonHandler handler = new JsonHandler();
			List<CityDistricts> cityDistricts = m_storage.CityDistrictsDao.selectEntities();
			if(cityDistricts == null)
			{
				handler.appendError("Can not select City Districts");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			JArray jArray = new JArray();
			foreach (var item in cityDistricts)
			{
				JObject cityDistrict = m_jsonConvertor.CityDistrictJsonConvertor.toJson(item);
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
			JsonHandler handler = new JsonHandler();
			if (id < 0)
			{
				handler.appendError($"Id is less then 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;				
				return handler.getJson();
			}

			CityDistricts cityDistrict = m_storage.CityDistrictsDao.selectEntityById(id);
			JObject jObject = new JObject();

			if (cityDistrict == null)
			{
				handler.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}
			jObject = m_jsonConvertor.CityDistrictJsonConvertor.toJson(cityDistrict);

			return jObject.ToString();
        }

		// POST:api/v1/CityDistricts
		[HttpPost]
        public string Post([FromBody] JObject value)
        {
			JsonHandler handler = new JsonHandler();
			if (value == null)
			{
				handler.appendError($"JSON parametr is null");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			CityDistricts newCityDistrict = m_jsonConvertor.CityDistrictJsonConvertor.fromJson(value);
			if (newCityDistrict == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			int id = m_storage.CityDistrictsDao.insertEntity(newCityDistrict);
			if(id < 0)
			{
				handler.appendError($"Can not insert City District with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
        }

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

			CityDistricts cityDistrict = m_storage.CityDistrictsDao.selectEntityById(id);
			if (cityDistrict == null)
			{
				handler.appendError($"Can not find City District with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			CityDistricts newCityDistrict = m_jsonConvertor.CityDistrictJsonConvertor.fromJson(value);
			if(newCityDistrict == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			cityDistrict.Name = newCityDistrict.Name;

			bool result = m_storage.CityDistrictsDao.updateEntity(cityDistrict);
			if(result == false)
			{
				handler.appendError($"Can not update City District with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

		// DELETE: api/v1/ApiWithActions/5
		[HttpDelete("{id}")]
        public string Delete(int id)
        {
			JsonHandler handler = new JsonHandler();
			if (id < 0)
			{
				handler.appendError($"Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			bool result = m_storage.CityDistrictsDao.deleteEntityById(id);

			if(result == false)
			{
				handler.appendError($"Can not delete City District with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
        }
    }
}
