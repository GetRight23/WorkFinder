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
		private DBContext context;
		private Storage storage;
		private JsonConvertorEngine jsonConvertor;

		public AddressController(DBContext context, Storage storage, JsonConvertorEngine jsonConvertor)
		{
			this.context = context;
			this.storage = storage;
			this.jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/Address - select all
		[HttpGet]
        public string Get()
        {
			JsonWrapper wrapper = new JsonWrapper();
			List<Address> addresses = storage.AddressDao.selectEntities();

			if (addresses == null)
			{
                wrapper.appendError("Can not select Addresses");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in addresses)
			{
				JObject address = jsonConvertor.AddressJsonConvertor.toJson(item);
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
			JsonWrapper wrapper = new JsonWrapper();

			if(id < 0)
			{
                wrapper.appendError($"Id is less then 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			Address address = storage.AddressDao.selectEntityById(id);
			
			if(address == null)
			{
                wrapper.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			JObject jObject = new JObject();
			jObject = jsonConvertor.AddressJsonConvertor.toJson(address);

            return jObject.ToString();
        }

		// POST: api/v1/Address - insert
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

			Address newAddress = jsonConvertor.AddressJsonConvertor.fromJson(value);

			if(newAddress == null)
			{
                wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			int id = storage.AddressDao.insertEntity(newAddress);

			if(id < 0)
			{
                wrapper.appendError($"Can not insert Address with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
        }

		// POST: api/v1/Address/id - update by id
		[HttpPost("{id}")]
		public string Post(int id, [FromBody] JObject value)
		{
			JsonWrapper wrapper = new JsonWrapper();

			if(id < 0)
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

			Address address = storage.AddressDao.selectEntityById(id);

			if(address == null)
			{
                wrapper.appendError($"Can not find Address with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			Address newAddress = jsonConvertor.AddressJsonConvertor.fromJson(value);

			if(newAddress == null)
			{
                wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			address.ApptNum = newAddress.ApptNum;
			address.IdCity = newAddress.IdCity;
			address.IdCityDistrict = newAddress.IdCityDistrict;
			address.StreetName = newAddress.StreetName;

			bool result = storage.AddressDao.updateEntity(address);

			if(result == false)
			{
                wrapper.appendError($"Can not update Address with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

		// DELETE: api/v1/Address/5 - delete by id
		[HttpDelete("{id}")]
        public string Delete(int id)
        {
			JsonWrapper wrapper = new JsonWrapper();

			if(id < 0)
			{
                wrapper.appendError($"Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			bool result = storage.AddressDao.deleteEntityById(id);

			if(result == false)
			{
                wrapper.appendError($"Can not delete Address with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
        }
    }
}
