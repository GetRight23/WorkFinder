﻿using System;
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
    public class OrderController : ControllerBase
    {
		private DBContext m_context;
		private Storage m_storage;
		private JsonConvertorEngine m_jsonConvertor;

		public OrderController(DBContext dBContext, Storage storage, JsonConvertorEngine jsonConvertor)
		{
			m_context = dBContext;
			m_storage = storage;
			m_jsonConvertor = jsonConvertor;
		}


		// GET: api/v1/Order - select all
		[HttpGet]
        public string Get()
        {
			JsonHandler handler = new JsonHandler();
			List<Order> addresses = m_storage.OrderDao.selectEntities();

			if (addresses == null)
			{
				handler.appendError("Can not select Orders");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in addresses)
			{
				JObject order = m_jsonConvertor.OrderConvertor.toJson(item);
				if (order != null)
				{
					jArray.Add(order);
				}
			}

			return jArray.ToString();
		}

        // GET: api/v1/Order/5 - select by id
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

			Order order = m_storage.OrderDao.selectEntityById(id);

			if (order == null)
			{
				handler.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			JObject jObject = new JObject();
			jObject = m_jsonConvertor.OrderConvertor.toJson(order);

			return jObject.ToString();
        }

        // POST: api/v1/Order - insert
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

			Order newOrder = m_jsonConvertor.OrderConvertor.fromJson(value);

			if (newOrder == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			int id = m_storage.OrderDao.insertEntity(newOrder);

			if (id < 0)
			{
				handler.appendError($"Can not insert Order with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

        // PUT: api/v1/Order/5 - update
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

			Order order = m_storage.OrderDao.selectEntityById(id);

			if (order == null)
			{
				handler.appendError($"Can not find Order with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			Order newOrder = m_jsonConvertor.OrderConvertor.fromJson(value);

			if (newOrder == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			order.Info = newOrder.Info;
			order.IdOrderList = newOrder.IdOrderList;

			bool result = m_storage.OrderDao.updateEntity(order);

			if (result == false)
			{
				handler.appendError($"Can not update Order with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
        }

        // DELETE: api/v1/Delete/5 - delete by id
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

			bool result = m_storage.OrderDao.deleteEntityById(id);

			if (result == false)
			{
				handler.appendError($"Can not delete Order with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}
    }
}