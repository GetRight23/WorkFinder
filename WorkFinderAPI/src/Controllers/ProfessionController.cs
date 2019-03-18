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
    public class ProfessionController : ControllerBase
    {
		private DBContext context;
		private Storage storage;
		private JsonConvertorEngine jsonConvertor;

		public ProfessionController(DBContext context, Storage storage, JsonConvertorEngine jsonConvertor)
		{
			this.context = context;
			this.storage = storage;
			this.jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/Profession - select all
		[HttpGet]
        public string Get()
        {
			JsonWrapper wrapper = new JsonWrapper();
			List<Profession> professions = storage.ProfessionDao.selectEntities();

			if (professions == null)
			{
				wrapper.appendError("Can not select Professions");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in professions)
			{
				JObject profesion = jsonConvertor.ProfessionConvertor.toJson(item);
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
			JsonWrapper wrapper = new JsonWrapper();

			if (id < 0)
			{
				wrapper.appendError($"Id is less then 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			Profession profession = storage.ProfessionDao.selectEntityById(id);

			if (profession == null)
			{
				wrapper.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return wrapper.getJson();
			}

			JObject jObject = new JObject();
			jObject = jsonConvertor.ProfessionConvertor.toJson(profession);

			return jObject.ToString();
		}

        // POST: api/v1/Profession - insert 
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

			Profession newProfession = jsonConvertor.ProfessionConvertor.fromJson(value);

			if(newProfession == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			int id = storage.ProfessionDao.insertEntity(newProfession);

			if (id < 0)
			{
				wrapper.appendError($"Can not insert Profession with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}

        // PUT: api/v1/Profession/5 - update by id
        [HttpPost("{id}")]
        public string Post(int id, [FromBody] JObject value)
        {
			JsonWrapper wrapper = new JsonWrapper();

			if(id < 0)
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

			Profession profession = storage.ProfessionDao.selectEntityById(id);

			if(profession == null)
			{
				wrapper.appendError($"Can not find Profession with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			Profession newProfession = jsonConvertor.ProfessionConvertor.fromJson(value);

			if(newProfession == null)
			{
				wrapper.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			profession.Name = newProfession.Name;
			profession.IdProfCategory = newProfession.IdProfCategory;

			bool result = storage.ProfessionDao.updateEntity(profession);

			if(result == false)
			{
				wrapper.appendError($"Can not update Profession with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
        }

		// DELETE: api/v1/Profession/5 - delete by id
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

			bool result = storage.ProfessionDao.deleteEntityById(id);

			if (result == false)
			{
				wrapper.appendError($"Can not delete Profession with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return wrapper.getJson();
			}

			return null;
		}
    }
}
