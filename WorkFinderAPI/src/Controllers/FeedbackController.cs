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
    public class FeedbackController : ControllerBase
    {
		private DBContext m_context;
		private Storage m_storage;
		private JsonConvertorEngine m_jsonConvertor;

		public FeedbackController(DBContext dBContext, Storage storage, JsonConvertorEngine jsonConvertor)
		{
			m_context = dBContext;
			m_storage = storage;
			m_jsonConvertor = jsonConvertor;
		}

		// GET: api/v1/Feedback - select all
		[HttpGet]
        public string Get()
        {
			JsonHandler handler = new JsonHandler();
			List<Feedback> feedbacks = m_storage.FeedbackDao.selectEntities();

			if(feedbacks == null)
			{
				handler.appendError("Can not select Feedbacks");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			JArray jArray = new JArray();

			foreach (var item in feedbacks)
			{
				JObject feedback = m_jsonConvertor.FeedbackConvertor.toJson(item);
				if (feedback != null)
				{
					jArray.Add(feedback);
				}
			}

			return jArray.ToString();
        }

		// GET: api/v1/Feedback/5 - select by id
		[HttpGet("{id}")]
        public string Get(int id)
        {
			JsonHandler handler = new JsonHandler();

			if(id < 0)
			{
				handler.appendError($"Id is less then 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			Feedback feedback = m_storage.FeedbackDao.selectEntityById(id);

			if(feedback == null)
			{
				handler.appendError($"Can not find id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				return handler.getJson();
			}

			JObject jObject = new JObject();
			jObject = m_jsonConvertor.FeedbackConvertor.toJson(feedback);

			return jObject.ToString();
        }

		// POST: api/v1/Feedback - insert
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

			Feedback newFeedback = m_jsonConvertor.FeedbackConvertor.fromJson(value);

			if(newFeedback == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			int id = m_storage.FeedbackDao.insertEntity(newFeedback);

			if (id < 0)
			{
				handler.appendError($"Can not insert Feedback with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

		// POST: api/v1/Feedback/id - update by id
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

			Feedback feedback = m_storage.FeedbackDao.selectEntityById(id);

			if(feedback == null)
			{
				handler.appendError($"Can not find Feedback with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			Feedback newFeedback = m_jsonConvertor.FeedbackConvertor.fromJson(value);

			if(newFeedback == null)
			{
				handler.appendError($"Unsuccessful convertaion from JSON");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			feedback.Name = newFeedback.Name;
			feedback.Patronymic = newFeedback.Patronymic;
			feedback.GradeValue = newFeedback.GradeValue;
			feedback.Date = newFeedback.Date;
			feedback.Text = newFeedback.Text;

			bool result = m_storage.FeedbackDao.updateEntity(feedback);

			if(result == false)
			{
				handler.appendError($"Can not update Feedback with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
		}

		// DELETE: api/v1/Feedback/5 - delete by id
		[HttpDelete("{id}")]
        public string Delete(int id)
        {
			JsonHandler handler = new JsonHandler();

			if(id < 0)
			{
				handler.appendError($"Id is less than 0");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			bool result = m_storage.FeedbackDao.deleteEntityById(id);

			if(result == false)
			{
				handler.appendError($"Can not delete Feedback with id {id}");
				HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				return handler.getJson();
			}

			return null;
        }
    }
}
