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
    public class ProfessionCategoryController : ControllerBase
    {
		private Storage storage;
		private JsonConvertorEngine jsonConvertor;

		public ProfessionCategoryController(Storage storage, JsonConvertorEngine jsonConvertor)
		{
			this.storage = storage;
			this.jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/ProfessionCategory - select all
		[HttpGet]
        public string Get()
        {
			JsonWrapper wrapper = new JsonWrapper();
			List<ProfessionCategory> professions = storage.ProfessionCategoryDao.selectEntities();

			if (professions == null)
			{
				wrapper.appendError("Can not select Profession categories");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in professions)
			{
				JObject profesionCategory = jsonConvertor.ProfessionCategoryConvertor.toJson(item);
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
			JsonWrapper wrapper = new JsonWrapper();

			if (id < 0)
			{
				wrapper.appendError($"Id is less then 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			ProfessionCategory professionCategory = storage.ProfessionCategoryDao.selectEntityById(id);

			if (professionCategory == null)
			{
				wrapper.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			JObject jObject = new JObject();
			jObject = jsonConvertor.ProfessionCategoryConvertor.toJson(professionCategory);

			return jObject.ToString();
		}

		// POST: api/v1/ProfessionCategory - insert 
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

			ProfessionCategory newProfessionCategory = jsonConvertor.ProfessionCategoryConvertor.fromJson(value);

			if (newProfessionCategory == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			int id = storage.ProfessionCategoryDao.insertEntity(newProfessionCategory);

			if(id == 0)
			{
				wrapper.appendError($"Can not insert Profession Category with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
        }

		// PUT:api/v1/ProfessionCategory/5
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

			ProfessionCategory newProfessionCategory = jsonConvertor.ProfessionCategoryConvertor.fromJson(value);

			if (newProfessionCategory == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			newProfessionCategory.Id = id;

			bool result = storage.ProfessionCategoryDao.updateEntity(newProfessionCategory);

			if(result == false)
			{
				wrapper.appendError($"Can not update Profession Category with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
        }

		// DELETE: api/v1/ProfessionCategory/5
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

			bool result = storage.ProfessionCategoryDao.deleteEntityById(id);

			if (result == false)
			{
				wrapper.appendError($"Can not delete Profession Category with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
        }
    }
}
