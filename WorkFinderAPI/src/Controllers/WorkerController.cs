using System.Collections.Generic;
using System.Net;
using DatabaseDao;
using JsonConvertor;
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
		private Storage storage;
		private JsonConvertorEngine jsonConvertor;

		public WorkerController(Storage storage, JsonConvertorEngine jsonConvertor)
		{
			this.storage = storage;
			this.jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/Worker - select all
		[HttpGet]
		public string Get()
		{
			JsonWrapper wrapper = new JsonWrapper();
			List<Worker> workers = storage.WorkerDao.selectEntities();

			if (workers == null)
			{
				wrapper.appendError("Can not select Workers");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in workers)
			{
				JObject worker = jsonConvertor.WorkerConvertor.toJson(item);
				if (worker != null)
				{
					jArray.Add(worker);
				}
			}

			return wrapper.getJson(jArray);
		}

		// GET: api/v1/Worker/5 - select by id
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

			Worker worker= storage.WorkerDao.selectEntityById(id);

			if (worker == null)
			{
				wrapper.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			JObject jObject = new JObject();
			jObject = jsonConvertor.WorkerConvertor.toJson(worker);

			return jObject.ToString();
		}

		// GET: api/v1/Worker/5/Address - select worker by address id
		[HttpGet("{id}/Address")]
		public string GetAddressByWorkerId(int id)
		{
			JsonWrapper wrapper = new JsonWrapper();

			if (id < 0)
			{
				wrapper.appendError("Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			Worker worker = storage.WorkerDao.selectEntityById(id);

			if(worker == null)
			{
				wrapper.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			Address address = storage.AddressDao.selectEntityById(worker.IdAddress);

			if(address == null)
			{
				wrapper.appendError($"Can not find address id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			JObject jObject = new JObject();
			jObject = jsonConvertor.AddressJsonConvertor.toJson(address);

			return wrapper.getJson(jObject);
		}


		// POST: api/v1/Worker - insert
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

			Worker newWorker = jsonConvertor.WorkerConvertor.fromJson(value);

			if (newWorker == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			int id = storage.WorkerDao.insertEntity(newWorker);

			if (id <= 0)
			{
				wrapper.appendError($"Can not insert Worker with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

		// POST: api/v1/Worker/id - update by id
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

			Worker newWorker= jsonConvertor.WorkerConvertor.fromJson(value);

			if (newWorker== null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			newWorker.Id = id;

			bool result = storage.WorkerDao.updateEntity(newWorker);

			if (result == false)
			{
				wrapper.appendError($"Can not update Worker with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

		// DELETE: api/v1/Worker/5 - delete by id
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

			bool result = storage.WorkerDao.deleteEntityById(id);

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
