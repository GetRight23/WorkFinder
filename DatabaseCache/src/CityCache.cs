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
		public CityCache(Storage storage, DatabaseDaoImpl<City> cityDao) : base(storage, cityDao) { citiesCache = new Dictionary<int, string>(); }

		public override void updateCache()
		{			
			entities = entitiesDao.selectEntities().ToList();
			JArray jArray = new JArray();
			foreach (var item in entities)
			{
				JObject json = jsonConvertor.CityJsonConvertor.toJson(item);
				jArray.Add(json);
				citiesCache.Add(item.Id, json.ToString());
			}
			CachedJson = jArray.ToString();			
		}
	}
}
