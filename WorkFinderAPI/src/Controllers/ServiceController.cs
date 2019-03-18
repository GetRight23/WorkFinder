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
	public class ServiceController : ControllerBase
	{
		private DBContext m_context;
		private Storage m_storage;
		private JsonConvertorEngine m_jsonConvertor;

		public ServiceController(DBContext dBContext, Storage storage, JsonConvertorEngine jsonConvertor)
		{
			m_context = dBContext;
			m_storage = storage;
			m_jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/Service - select all
		[HttpGet]
		public string Get()
		{
			JsonHandler handler = new JsonHandler();
			List<Service> services = m_storage.ServiceDao.selectEntities();

			if (services == null)
			{
				handler.appendError("Can not select Service");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in services)
			{
				JObject service = m_jsonConvertor.ServiceConvertor.toJson(item);
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
			JsonHandler handler = new JsonHandler();

			if (id < 0)
			{
				handler.appendError($"Id is less then 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			Service service = m_storage.ServiceDao.selectEntityById(id);

			if (service == null)
			{
				handler.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			JObject jObject = new JObject();
			jObject = m_jsonConvertor.ServiceConvertor.toJson(service);

			return jObject.ToString();
		}

		// POST: api/v1/Service - insert
		[HttpPost]
		public string Post([FromBody] JObject value)
		{
			JsonHandler handler = new JsonHandler();

			if (value == null)
			{
				handler.appendError("JSON parametr is null");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			Service newService= m_jsonConvertor.ServiceConvertor.fromJson(value);

			if (newService== null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			int id = m_storage.ServiceDao.insertEntity(newService);

			if (id < 0)
			{
				handler.appendError($"Can not insert Service with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

		// POST: api/v1/Service/id - update by id
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

			Service service = m_storage.ServiceDao.selectEntityById(id);

			if (service == null)
			{
				handler.appendError($"Can not find Service with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			Service newService = m_jsonConvertor.ServiceConvertor.fromJson(value);

			if (newService == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			service.Price = newService.Price;
			service.Name = newService.Name;
			service.IdProfession = newService.IdProfession;

			bool result = m_storage.ServiceDao.updateEntity(service);

			if (result == false)
			{
				handler.appendError($"Can not update Service with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

		// DELETE: api/v1/Service/5 - delete by id
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

			bool result = m_storage.ServiceDao.deleteEntityById(id);

			if (result == false)
			{
				handler.appendError($"Can not delete Worker with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}
	}
}
