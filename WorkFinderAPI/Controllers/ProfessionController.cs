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
    public class ProfessionController : ControllerBase
    {
		private DBContext m_context;
		private Storage m_storage;
		private JsonConvertor m_jsonConvertor = null;

		public ProfessionController(DBContext dBContext, Storage storage)
		{
			m_context = dBContext;
			m_storage = storage;
			m_jsonConvertor = new JsonConvertor();
		}

		// GET: api/v1/Profession - select all
		[HttpGet]
        public string Get()
        {
			JsonHandler handler = new JsonHandler();
			List<Profession> professions = m_storage.ProfessionDao.selectEntities();

			if (professions == null)
			{
				handler.appendError("Can not select Professions");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in professions)
			{
				JObject profesion = m_jsonConvertor.toJson(item);
				if (profesion != null)
				{
					jArray.Add(profesion);
				}
			}

			return jArray.ToString();
        }

		// GET: api/v1/Profession/5 - select by id
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

			Profession profession = m_storage.ProfessionDao.selectEntityById(id);

			if (profession == null)
			{
				handler.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			JObject jObject = new JObject();
			jObject = m_jsonConvertor.toJson(profession);

			return jObject.ToString();
		}

        // POST: api/v1/Profession - insert 
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

			Profession newProfession = m_jsonConvertor.fronJsonToProfession(value);

			if(newProfession == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			int id = m_storage.ProfessionDao.insertEntity(newProfession);

			if (id < 0)
			{
				handler.appendError($"Can not insert Profession with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

        // PUT: api/v1/Profession/5 - update by id
        [HttpPost("{id}")]
        public string Post(int id, [FromBody] JObject value)
        {
			JsonHandler handler = new JsonHandler();

			if(id < 0)
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

			Profession profession = m_storage.ProfessionDao.selectEntityById(id);

			if(profession == null)
			{
				handler.appendError($"Can not find Profession with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			Profession newProfession = m_jsonConvertor.fronJsonToProfession(value);

			if(newProfession == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			profession.Name = newProfession.Name;
			profession.IdProfCategory = newProfession.IdProfCategory;

			bool result = m_storage.ProfessionDao.updateEntity(profession);

			if(result == false)
			{
				handler.appendError($"Can not update Profession with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}


			return null;
        }

		// DELETE: api/v1/Profession/5 - delete by id
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

			bool result = m_storage.ProfessionDao.deleteEntityById(id);

			if (result == false)
			{
				handler.appendError($"Can not delete Profession with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}
    }
}
