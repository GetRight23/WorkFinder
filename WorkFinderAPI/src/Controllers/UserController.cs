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
	public class UserController : ControllerBase
	{
		private DBContext m_context;
		private Storage m_storage;
		private JsonConvertorEngine m_jsonConvertor;

		public UserController(DBContext dBContext, Storage storage, JsonConvertorEngine jsonConvertor)
		{
			m_context = dBContext;
			m_storage = storage;
			m_jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/User - select all
		[HttpGet]
		public string Get()
		{
			JsonHandler handler = new JsonHandler();
			List<User> users = m_storage.UserDao.selectEntities();

			if (users == null)
			{
				handler.appendError("Can not select Users");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in users)
			{
				JObject user = m_jsonConvertor.UserConvertor.toJson(item);
				if (user != null)
				{
					jArray.Add(user);
				}
			}

			return jArray.ToString();
		}

		// GET: api/v1/User/5 - select by id
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

			User user = m_storage.UserDao.selectEntityById(id);

			if (user == null)
			{
				handler.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			JObject jObject = new JObject();
			jObject = m_jsonConvertor.UserConvertor.toJson(user);

			return jObject.ToString();
		}

		// POST: api/v1/User - insert
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

			User newUser = m_jsonConvertor.UserConvertor.fromJson(value);

			if (newUser== null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			int id = m_storage.UserDao.insertEntity(newUser);

			if (id < 0)
			{
				handler.appendError($"Can not insert User with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

		// POST: api/v1/User/id - update by id
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

			User user = m_storage.UserDao.selectEntityById(id);

			if (user == null)
			{
				handler.appendError($"Can not find User with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			User newUser= m_jsonConvertor.UserConvertor.fromJson(value);

			if (newUser == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			user.Login = newUser.Login;
			user.Password = newUser.Password;

			bool result = m_storage.UserDao.updateEntity(user);

			if (result == false)
			{
				handler.appendError($"Can not update User with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

		// DELETE: api/v1/User/5 - delete by id
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

			bool result = m_storage.UserDao.deleteEntityById(id);

			if (result == false)
			{
				handler.appendError($"Can not delete User with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}
	}
}
