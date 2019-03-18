using System.Collections.Generic;
using DatabaseDao;
using Models;
using System.Linq;
using JSONConvertor;
using Newtonsoft.Json.Linq;

namespace DatabaseCache
{
	public abstract class CacheDao<Type> : IDatabaseDao<Type> where Type : DBObject
	{
		public static Storage Storage { get; private set; }
		protected List<Type> m_entities = null;
		protected DatabaseDao<Type> m_entitiesDao = null;
		protected JsonConvertorEngine m_jsonConvertor = null;
		public string CachedJson { get; protected set; }

		public CacheDao(Storage storage, DatabaseDao<Type> entitiesDao)
		{
			Storage = storage;
			m_entitiesDao = entitiesDao;
			m_jsonConvertor = new JsonConvertorEngine();
			CachedJson = null;
		}

		public abstract void updateCache();

		public Type selectEntityById(int id)
		{
			if (m_entities == null)
			{
				updateCache();
			}

			return m_entities.Where(e => e.getId() == id).Single();
		}

		public List<Type> selectEntities()
		{
			if (m_entities == null)
			{
				updateCache();
			}

			return m_entities;
		}

		public List<Type> selectEntitiesByIds(List<int> ids)
		{
			List<Type> result = new List<Type>();

			if(m_entities == null)
			{
				updateCache();
			}
			
			foreach (var item in ids)
			{
				result.Add(m_entities.Where(e => e.getId() == item).Single());
			}

			return result;
		}

		public bool deleteEntityById(int id)
		{
			bool result = m_entitiesDao.deleteEntityById(id);
			if (result == true)
			{
				updateCache();
			}

			return result;
		}

		public bool deleteEntitiesByIds(List<int> ids)
		{
			bool result = m_entitiesDao.deleteEntitiesByIds(ids);
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
				insertedId = m_entitiesDao.insertEntity(entity);
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
				Type entity = m_entities.Where(e => e.getId() == item.getId()).Single();
				if(entity != null)
				{
					m_entitiesDao.insertEntity(entity);
					insertedIds.Add(entity.getId());
				}			
			}

			updateCache();

			return insertedIds;
		}

		public bool updateEntity(Type entity)
		{
			bool result = m_entitiesDao.updateEntity(entity);
			
			if(result != false)
			{
				updateCache();
			}			

			return result;
		}

		public bool updateEntities(List<Type> entities)
		{
			bool result = m_entitiesDao.updateEntities(entities);

			if (result != false)
			{
				updateCache();
			}

			return result;
		}
	}
}
