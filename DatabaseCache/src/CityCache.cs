using DatabaseDao;
using Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseCache
{
	public class CityCache : CacheDao<City>
	{
		public Dictionary<int, string> citiesCache = null;
		public CityCache(Storage storage, DatabaseDao<City> cityDao) : base(storage, cityDao) { citiesCache = new Dictionary<int, string>(); }

		public override void updateCache()
		{			
			m_entities = m_entitiesDao.selectEntities().ToList();
			JArray jArray = new JArray();
			foreach (var item in m_entities)
			{
				JObject json = m_jsonConvertor.toJson(item);
				jArray.Add(json);
				citiesCache.Add(item.Id, json.ToString());
			}
			CachedJson = jArray.ToString();			
		}
	}
}
