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
    public class ProfessionCategoryController : ControllerBase
    {
		private DBContext m_context;
		private Storage m_storage;
		private JsonConvertorEngine m_jsonConvertor;

		public ProfessionCategoryController(DBContext dBContext, Storage storage, JsonConvertorEngine jsonConvertor)
		{
			m_context = dBContext;
			m_storage = storage;
			m_jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/ProfessionCategory - select all
		[HttpGet]
        public string Get()
        {
			JsonHandler handler = new JsonHandler();
			List<ProfessionCategory> professions = m_storage.ProfessionCategoryDao.selectEntities();

			if (professions == null)
			{
				handler.appendError("Can not select Profession categories");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in professions)
			{
				JObject profesionCategory = m_jsonConvertor.ProfessionCategoryConvertor.toJson(item);
				if (profesionCategory != null)
				{
					jArray.Add(profesionCategory);
				}
			}

			return jArray.ToString();
        }

		// GET: api/v1/ProfessionCategory/5 - select by id
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

			ProfessionCategory professionCategory = m_storage.ProfessionCategoryDao.selectEntityById(id);

			if (professionCategory == null)
			{
				handler.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			JObject jObject = new JObject();
			jObject = m_jsonConvertor.ProfessionCategoryConvertor.toJson(professionCategory);

			return jObject.ToString();
		}

		// POST: api/v1/ProfessionCategory - insert 
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

			ProfessionCategory newProfessionCategory = m_jsonConvertor.ProfessionCategoryConvertor.fromJson(value);

			if (newProfessionCategory == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			int id = m_storage.ProfessionCategoryDao.insertEntity(newProfessionCategory);

			if(id < 0)
			{
				handler.appendError($"Can not insert Profession Category with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
        }

		// PUT:api/v1/ProfessionCategory/5
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

			ProfessionCategory professionCategory = m_storage.ProfessionCategoryDao.selectEntityById(id);

			if(professionCategory == null)
			{
				handler.appendError($"Can not find Profession Category with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			ProfessionCategory newProfessionCategory = m_jsonConvertor.ProfessionCategoryConvertor.fromJson(value);

			if (newProfessionCategory == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			professionCategory.Name = newProfessionCategory.Name;

			bool result = m_storage.ProfessionCategoryDao.updateEntity(professionCategory);

			if(result == false)
			{
				handler.appendError($"Can not update Profession Category with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
        }

		// DELETE: api/v1/ProfessionCategory/5
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

			bool result = m_storage.ProfessionCategoryDao.deleteEntityById(id);

			if (result == false)
			{
				handler.appendError($"Can not delete Profession Category with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
        }
    }
}
