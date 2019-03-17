using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DatabaseDao;
using JSONConvertor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json.Linq;

namespace WorkFinderAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
		private DBContext m_context;
		private Storage m_storage;
		private JsonConvertor m_jsonConvertor = null;

		public AddressController(DBContext dBContext, Storage storage)
		{
			m_context = dBContext;
			m_storage = storage;
			m_jsonConvertor = new JsonConvertor();
		}

		// GET: api/v1/Address - select all
		[HttpGet]
        public string Get()
        {
			JsonHandler handler = new JsonHandler();
			List<Address> addresses = m_storage.AddressDao.selectEntities();

			if (addresses == null)
			{
				handler.appendError("Can not select Addresses");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in addresses)
			{
				JObject address = m_jsonConvertor.toJson(item);
				if (address != null)
				{
					jArray.Add(address);
				}
			}

			return jArray.ToString();
        }

		// GET: api/v1/Address/5 - select by id
		[HttpGet("{id}")]
        public string Get(int id)
        {
			JsonHandler handler = new JsonHandler();

			if(id < 0)
			{
				handler.appendError($"Id is less then 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			Address address = m_storage.AddressDao.selectEntityById(id);
			
			if(address == null)
			{
				handler.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			JObject jObject = new JObject();
			jObject = m_jsonConvertor.toJson(address);

            return jObject.ToString();
        }

		// POST: api/v1/Address - insert
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

			Address newAddress = m_jsonConvertor.fromJsonToAddress(value);

			if(newAddress == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			int id = m_storage.AddressDao.insertEntity(newAddress);

			if(id < 0)
			{
				handler.appendError($"Can not insert Address with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
        }

		// POST: api/v1/Address/id - update by id
		[HttpPost("{id}")]
		public string Post(int id, [FromBody] JObject value)
		{
			JsonHandler handler = new JsonHandler();

			if(id < 0)
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

			Address address = m_storage.AddressDao.selectEntityById(id);
			if(address == null)
			{
				handler.appendError($"Can not find Address with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			Address newAddress = m_jsonConvertor.fromJsonToAddress(value);
			if(newAddress == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			address.ApptNum = newAddress.ApptNum;
			address.IdCity = newAddress.IdCity;
			address.IdCityDistrict = newAddress.IdCityDistrict;
			address.StreetName = newAddress.StreetName;

			bool result = m_storage.AddressDao.updateEntity(address);
			if(result == false)
			{
				handler.appendError($"Can not update Address with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

        // DELETE: api/ApiWithActions/5 - delete by id
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
			JsonHandler handler = new JsonHandler();

			if(id < 0)
			{
				handler.appendError($"Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			bool result = m_storage.AddressDao.deleteEntityById(id);

			if(result == false)
			{
				handler.appendError($"Can not delete Address with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
        }
    }
}
