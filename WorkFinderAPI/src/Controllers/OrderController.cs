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
    public class OrderController : ControllerBase
    {
		private Storage storage;
		private JsonConvertorEngine jsonConvertor;

		public OrderController(Storage storage, JsonConvertorEngine jsonConvertor)
		{
			this.storage = storage;
			this.jsonConvertor = jsonConvertor;
		}


		// GET: api/v1/Order - select all
		[HttpGet]
        public string Get()
        {
			JsonWrapper wrapper = new JsonWrapper();
			List<Order> addresses = storage.OrderDao.selectEntities();

			if (addresses == null)
			{
				wrapper.appendError("Can not select Orders");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in addresses)
			{
				JObject order = jsonConvertor.OrderConvertor.toJson(item);
				if (order != null)
				{
					jArray.Add(order);
				}
			}

			return wrapper.getJson(jArray);
		}

        // GET: api/v1/Order/5 - select by id
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

			Order order = storage.OrderDao.selectEntityById(id);

			if (order == null)
			{
				wrapper.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			JObject jObject = new JObject();
			jObject = jsonConvertor.OrderConvertor.toJson(order);

			return jObject.ToString();
        }

        // POST: api/v1/Order - insert
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

			Order newOrder = jsonConvertor.OrderConvertor.fromJson(value);

			if (newOrder == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			int id = storage.OrderDao.insertEntity(newOrder);

			if (id == 0)
			{
				wrapper.appendError($"Can not insert Order with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

        // PUT: api/v1/Order/5 - update
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

			Order newOrder = jsonConvertor.OrderConvertor.fromJson(value);

			if (newOrder == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			newOrder.Id = id;

			bool result = storage.OrderDao.updateEntity(newOrder);

			if (result == false)
			{
				wrapper.appendError($"Can not update Order with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
        }

        // DELETE: api/v1/Delete/5 - delete by id
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

			bool result = storage.OrderDao.deleteEntityById(id);

			if (result == false)
			{
				wrapper.appendError($"Can not delete Order with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}
    }
}
