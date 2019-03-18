using System.Collections.Generic;
using DatabaseDao;
using Models;
using System.Linq;
using JSONConvertor;
using Newtonsoft.Json.Linq;

namespace DatabaseCache
{
	public abstract class CacheDao<Type> : DatabaseDao<Type> where Type : DBObject
	{
		public static Storage Storage { get; private set; }
		protected List<Type> entities = null;
		protected DatabaseDaoImpl<Type> entitiesDao = null;
		protected JsonConvertorEngine jsonConvertor = null;
		public string CachedJson { get; protected set; }

		public CacheDao(Storage storage, DatabaseDaoImpl<Type> entitiesDao)
		{
			Storage = storage;
			this.entitiesDao = entitiesDao;
			jsonConvertor = new JsonConvertorEngine();
			CachedJson = null;
		}

		public abstract void updateCache();

		public Type selectEntityById(int id)
		{
			if (entities == null)
			{
				updateCache();
			}

			return entities.Where(e => e.getId() == id).Single();
		}

		public List<Type> selectEntities()
		{
			if (entities == null)
			{
				updateCache();
			}

			return entities;
		}

		public List<Type> selectEntitiesByIds(List<int> ids)
		{
			List<Type> result = new List<Type>();

			if(entities == null)
			{
				updateCache();
			}
			
			foreach (var item in ids)
			{
				result.Add(entities.Where(e => e.getId() == item).Single());
			}

			return result;
		}

		public bool deleteEntityById(int id)
		{
			bool result = entitiesDao.deleteEntityById(id);
			if (result == true)
			{
				updateCache();
			}

			return result;
		}

		public bool deleteEntitiesByIds(List<int> ids)
		{
			bool result = entitiesDao.deleteEntitiesByIds(ids);
			if (result == true)
			{
				updateCache();
			}

			return result;
		}

		public int insertEntity(Type entity)
		{
			int insertedId = 0;
			if(entity != null)
			{				
				insertedId = entitiesDao.insertEntity(entity);
				if(insertedId != 0)
				{
					updateCache();
				}				
			}		
			
			return insertedId;
		}

		public List<int> insertEntities(List<Type> entities)
		{
			List<int> insertedIds = new List<int>();			

			foreach (var item in entities)
			{
				Type entity = this.entities.Where(e => e.getId() == item.getId()).Single();
				if(entity != null)
				{
					entitiesDao.insertEntity(entity);
					insertedIds.Add(entity.getId());
				}			
			}

			updateCache();

			return insertedIds;
		}

		public bool updateEntity(Type entity)
		{
			bool result = entitiesDao.updateEntity(entity);
			
			if(result != false)
			{
				updateCache();
			}			

			return result;
		}

		public bool updateEntities(List<Type> entities)
		{
			bool result = entitiesDao.updateEntities(entities);

			if (result != false)
			{
				updateCache();
			}

			return result;
		}
	}
}
