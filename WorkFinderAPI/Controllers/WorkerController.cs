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
	public class WorkerController : ControllerBase
	{
		private DBContext m_context;
		private Storage m_storage;
		private JsonConvertor m_jsonConvertor = null;

		public WorkerController(DBContext dBContext, Storage storage)
		{
			m_context = dBContext;
			m_storage = storage;
			m_jsonConvertor = new JsonConvertor();
		}

		// GET: api/v1/Worker - select all
		[HttpGet]
		public string Get()
		{
			JsonHandler handler = new JsonHandler();
			List<Worker> workers = m_storage.WorkerDao.selectEntities();

			if (workers == null)
			{
				handler.appendError("Can not select Workers");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in workers)
			{
				JObject worker = m_jsonConvertor.toJson(item);
				if (worker != null)
				{
					jArray.Add(worker);
				}
			}

			return jArray.ToString();
		}

		// GET: api/v1/Worker/5 - select by id
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

			Worker worker= m_storage.WorkerDao.selectEntityById(id);

			if (worker == null)
			{
				handler.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			JObject jObject = new JObject();
			jObject = m_jsonConvertor.toJson(worker);

			return jObject.ToString();
		}

		// POST: api/v1/Worker - insert
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

			Worker newWorker = m_jsonConvertor.fromJsonToWorker(value);

			if (newWorker == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			int id = m_storage.WorkerDao.insertEntity(newWorker);

			if (id < 0)
			{
				handler.appendError($"Can not insert Worker with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

		// POST: api/v1/Worker/id - update by id
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

			Worker worker = m_storage.WorkerDao.selectEntityById(id);

			if (worker == null)
			{
				handler.appendError($"Can not find Worker with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			Worker newWorker= m_jsonConvertor.fromJsonToWorker(value);

			if (newWorker== null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			worker.Name = newWorker.Name;
			worker.LastName = newWorker.LastName;
			worker.PhoneNumber = newWorker.PhoneNumber;
			worker.Info = newWorker.Info;
			worker.IdAddress = newWorker.IdAddress;
			worker.IdUser= newWorker.IdUser;

			bool result = m_storage.WorkerDao.updateEntity(worker);

			if (result == false)
			{
				handler.appendError($"Can not update Worker with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

		// DELETE: api/v1/Worker/5 - delete by id
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

			bool result = m_storage.WorkerDao.deleteEntityById(id);

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
