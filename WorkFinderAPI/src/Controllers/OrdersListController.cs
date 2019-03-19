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
	public class OrdersListController : ControllerBase
	{
		private Storage storage;
		private JsonConvertorEngine jsonConvertor;

		public OrdersListController(Storage storage, JsonConvertorEngine jsonConvertor)
		{
			this.storage = storage;
			this.jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/OrdersList - select all
		[HttpGet]
		public string Get()
		{
			JsonWrapper wrapper = new JsonWrapper();
			List<OrdersList> ordersLists = storage.OrderListDao.selectEntities();

			if (ordersLists == null)
			{
				wrapper.appendError("Can not select Orders Lists");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in ordersLists)
			{
				JObject ordersList = jsonConvertor.OrdersListConvertor.toJson(item);
				if (ordersList != null)
				{
					jArray.Add(ordersList);
				}
			}

			return wrapper.getJson(jArray);
		}

		// GET: api/v1/OrdersList/5 - select by id
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

			OrdersList ordersList = storage.OrderListDao.selectEntityById(id);

			if (ordersList == null)
			{
				wrapper.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			JObject jObject = new JObject();
			jObject = jsonConvertor.OrdersListConvertor.toJson(ordersList);

			return jObject.ToString();
		}

		[HttpGet("{id}/Worker")]
		public string GetWorkerByOrdersListId(int id)
		{
			JsonWrapper wrapper = new JsonWrapper();

			if (id < 0)
			{
				wrapper.appendError("Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			OrdersList ordersList = this.storage.OrderListDao.selectEntityById(id);

			if (ordersList == null)
			{
				wrapper.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			Worker worker = storage.WorkerDao.selectEntityById(ordersList.IdWorker);

			if (worker == null)
			{
				wrapper.appendError($"Can not find worker id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			JObject jObject = new JObject();
			jObject = jsonConvertor.WorkerConvertor.toJson(worker);

			return wrapper.getJson(jObject);
		}

		// POST: api/v1/OrdersList - insert
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

			OrdersList newOrdersList = jsonConvertor.OrdersListConvertor.fromJson(value);

			if (newOrdersList == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			int id = storage.OrderListDao.insertEntity(newOrdersList);

			if (id == 0)
			{
				wrapper.appendError($"Can not insert Orders List with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

		// POST: api/v1/OrdersList/id - update by id
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

			OrdersList newOrdersList= jsonConvertor.OrdersListConvertor.fromJson(value);

			if (newOrdersList == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			newOrdersList.Id = id;

			bool result = storage.OrderListDao.updateEntity(newOrdersList);

			if (result == false)
			{
				wrapper.appendError($"Can not update Orders List with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

		// DELETE: api/v1/OrdersList/5 - delete by id
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

			bool result = storage.OrderListDao.deleteEntityById(id);

			if (result == false)
			{
				wrapper.appendError($"Can not delete Orders List with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}
	}
}
