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
using DatabaseCache;

namespace WebApplication1.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class CityController : ControllerBase
	{
		private DBContext m_context;
		private Storage m_storage;
		private CityCache m_cache = null;

		public CityController(DBContext dBContext, Storage storage)
		{
			m_context = dBContext;
			m_storage = storage;
			m_cache = new CityCache(storage, storage.CityDao);
		}

		// GET api/v1/values
		[HttpGet]
		public ActionResult<string> Get()
		{
			m_cache.updateCache();
			return m_cache.CachedJson;
		}

		// GET api/v1/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			m_cache.updateCache();
			return m_cache.citiesCache[id];
		}

		// POST api/v1/values
		[HttpPost]
		public void Post([FromBody] JObject value)
		{
			JsonConvertor jsonConvertor = new JsonConvertor();
			City city = jsonConvertor.fromJsonToCity(value);
			int id = m_storage.CityDao.insertEntity(city);
		}

		// POST api/values
		[HttpPost("{id}")]
		public void Post(int id, [FromBody] JObject jObject)
		{
			JsonConvertor jsonConvertor = new JsonConvertor();
			City city = m_storage.CityDao.selectEntityById(id);
			City cityTemp = jsonConvertor.fromJsonToCity(jObject);
			city.Name = cityTemp.Name;
			m_storage.CityDao.updateEntity(city);
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{

		}
	}
}
