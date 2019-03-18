using System.Collections.Generic;
using System.Net;
using DatabaseDao;
using JsonConvertor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json.Linq;

namespace WorkFinderAPI.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class ServiceController : ControllerBase
	{
		private DBContext context;
		private Storage storage;
		private JsonConvertorEngine jsonConvertor;

		public ServiceController(DBContext context, Storage storage, JsonConvertorEngine jsonConvertor)
		{
			this.context = context;
			this.storage = storage;
			this.jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/Service - select all
		[HttpGet]
		public string Get()
		{
			JsonWrapper wrapper = new JsonWrapper();
			List<Service> services = storage.ServiceDao.selectEntities();

			if (services == null)
			{
				wrapper.appendError("Can not select Service");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in services)
			{
				JObject service = jsonConvertor.ServiceConvertor.toJson(item);
				if (service != null)
				{
					jArray.Add(service);
				}
			}

			return jArray.ToString();
		}

		// GET: api/v1/Serivce/5 - select by id
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

			Service service = storage.ServiceDao.selectEntityById(id);

			if (service == null)
			{
				wrapper.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			JObject jObject = new JObject();
			jObject = jsonConvertor.ServiceConvertor.toJson(service);

			return jObject.ToString();
		}

		// POST: api/v1/Service - insert
		[HttpPost]
		public string Post([FromBody] JObject value)
		{
			JsonWrapper wrapper = new JsonWrapper();

			if (value == null)
			{
				wrapper.appendError("JSON parametr is null");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			Service newService= jsonConvertor.ServiceConvertor.fromJson(value);

			if (newService== null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			int id = storage.ServiceDao.insertEntity(newService);

			if (id == 0)
			{
				wrapper.appendError($"Can not insert Service with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

		// POST: api/v1/Service/id - update by id
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

			Service service = storage.ServiceDao.selectEntityById(id);

			if (service == null)
			{
				wrapper.appendError($"Can not find Service with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			Service newService = jsonConvertor.ServiceConvertor.fromJson(value);

			if (newService == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			service.Price = newService.Price;
			service.Name = newService.Name;
			service.IdProfession = newService.IdProfession;
			//service = newService;
			//service.Id = id;

			bool result = storage.ServiceDao.updateEntity(service);

			if (result == false)
			{
				wrapper.appendError($"Can not update Service with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

		// DELETE: api/v1/Service/5 - delete by id
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

			bool result = storage.ServiceDao.deleteEntityById(id);

			if (result == false)
			{
				wrapper.appendError($"Can not delete Worker with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}
	}
}
