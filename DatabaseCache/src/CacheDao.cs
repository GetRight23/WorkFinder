using System.Collections.Generic;
using DatabaseDao;
using Models;
using System.Linq;

namespace DatabaseCache
{
	public class CacheDao<Type> : IDatabaseDao<Type> where Type : DBObject
	{
		public static Storage Storage { get; private set; }
		private List<Type> m_entities = null;
		private DatabaseDao<Type> m_entitiesDao = null;

		public CacheDao(Storage storage, DatabaseDao<Type> entitiesDao)
		{
			Storage = storage;
			m_entitiesDao = entitiesDao;
		}

		public Type selectEntityById(int id)
		{
			if (m_entities == null)
			{
				m_entities = m_entitiesDao.selectEntities().ToList();
			}			
			
			return m_entities.Where(e => e.getId() == id).Single();
		}

		public List<Type> selectEntities()
		{
			if (m_entities == null)
			{
				m_entities = m_entitiesDao.selectEntities().ToList();
			}

			return m_entities;
		}

		public List<Type> selectEntitiesByIds(List<int> ids)
		{
			List<Type> result = new List<Type>();
			if (m_entities == null)
			{
				m_entities = m_entitiesDao.selectEntities().ToList();				
			}

			foreach (var item in ids)
			{
				result.Add(m_entities.Where(e => e.getId() == item).Single());
			}

			return result;
		}

		public bool deleteEntityById(int id)
		{
			if (m_entities == null)
			{
				m_entities = m_entitiesDao.selectEntities().ToList();
			}

			bool result = m_entitiesDao.deleteEntityById(id);
			if (result == true)
			{
				Type entity = m_entities.Where(e => e.getId() == id).Single();
				if (entity != null)
				{
					m_entities.Remove(entity);
				}
			}

			return result;
		}

		public bool deleteEntitiesByIds(List<int> ids)
		{
			if (m_entities == null)
			{
				m_entities = m_entitiesDao.selectEntities().ToList();
			}
			bool result = m_entitiesDao.deleteEntitiesByIds(ids);
			if (result == true)
			{
				foreach (var item in ids)
				{
					Type entity = m_entities.Where(e => e.getId() == item).Single();
					if(entity != null)
					{
						m_entities.Remove(entity);
					}					
				}
			}

			return result;
		}

		public int insertEntity(Type entity)
		{
			if (m_entities == null)
			{
				m_entities = m_entitiesDao.selectEntities().ToList();
			}
			int insertedId = m_entitiesDao.insertEntity(entity);
			if(entity != null)
			{
				m_entities.Add(entity);
			}

			return insertedId;
		}

		public List<int> insertEntities(List<Type> entities)
		{
			List<int> insertedIds = new List<int>();
			if (m_entities == null)
			{
				m_entities = m_entitiesDao.selectEntities().ToList();
			}

			foreach (var item in entities)
			{
				Type entity = m_entities.Where(e => e.getId() == item.getId()).Single();
				if(entity != null)
				{
					insertedIds.Add(entity.getId());
				}			
			}

			return insertedIds;
		}

		public bool updateEntity(Type entity)
		{
			if (m_entities == null)
			{
				m_entities = m_entitiesDao.selectEntities().ToList();
			}
			bool result = m_entitiesDao.updateEntity(entity);
			
			if(result != false)
			{
				Type temp = m_entities
					.Where(e => e.getId() == entity.getId())
					.Select(e => { e = entity; return e; })
					.Single();
			}
			return result;
		}

		public bool updateEntities(List<Type> entities)
		{
			if (m_entities == null)
			{
				m_entities = m_entitiesDao.selectEntities().ToList();
			}
			bool result = m_entitiesDao.updateEntities(entities);

			if (result != false)
			{
				foreach (var item in entities)
				{
					Type temp = m_entities
					.Where(e => e.getId() == item.getId())
					.Select(e => { e = item; return e; })
					.Single();
				}	
			}
			return result;
		}
	}
}
