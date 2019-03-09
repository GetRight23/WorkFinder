using System.Collections.Generic;
using DatabaseDao;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;

namespace DatabaseCache
{
	public class CacheDao<Type> : IDatabaseDao<Type> where Type : DBObject
	{
		public static Storage Storage { get; private set; }
		private List<Type> m_entities = null;
		private DbSet<Type> m_entitiesDao = null;

		public CacheDao(Storage storage)
		{
			Storage = storage;
		}

		public Type selectEntityById(int id)
		{
			throw new System.NotImplementedException();
		}

		public List<Type> selectEntities()
		{
			if (m_entities == null)
			{
				m_entities = m_entitiesDao.ToList();
			}

			return m_entities;
		}

		public List<Type> selectEntitiesByIds(List<int> ids)
		{
			throw new System.NotImplementedException();
		}

		public bool deleteEntityById(int id)
		{
			throw new System.NotImplementedException();
		}

		public bool deleteEntitiesByIds(List<int> ids)
		{
			throw new System.NotImplementedException();
		}

		public int insertEntity(Type entity)
		{
			throw new System.NotImplementedException();
		}

		public List<int> insertEntities(List<Type> entities)
		{
			throw new System.NotImplementedException();
		}

		public bool updateEntity(Type entity)
		{
			throw new System.NotImplementedException();
		}

		public bool updateEntities(List<Type> entities)
		{
			throw new System.NotImplementedException();
		}
	}
}
