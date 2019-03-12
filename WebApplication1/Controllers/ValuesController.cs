using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseDao;
using Microsoft.AspNetCore.Mvc;
using DatabaseConfiguration;
using JSONConvertor;
using Models;
using Newtonsoft.Json.Linq;

namespace WebApplication1.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		// GET api/v1/values
		[HttpGet]
		public ActionResult<string> Get()
		{
			Storage storage = new PostgreStorage(Configuration.TestConnection);
			List<City> city = storage.CityDao.selectEntities();
			JArray jArray = new JArray();
			JsonConvertor jsonConvertor = new JsonConvertor();			
			foreach (var item in city)
			{
				JObject cityJson = jsonConvertor.toJson(item);
				jArray.Add(cityJson);
			}		
			return jArray.ToString();
		}

		// GET api/v1/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			Storage storage = new PostgreStorage(Configuration.TestConnection);
			City city = storage.CityDao.selectEntityById(id);
			if(city == null)
			{
				return new JObject().ToString();
			}
			JsonConvertor jsonConvertor = new JsonConvertor();
			JObject cityJson  = jsonConvertor.toJson(city);
			return cityJson.ToString();
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// POST api/values/5
		[HttpPost("{id}")]
		public void Post([FromBody] string value, int id)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
