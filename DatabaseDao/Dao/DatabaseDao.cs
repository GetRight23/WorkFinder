using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace DatabaseDao
{
	public class DatabaseDao<Type> where Type : DBObject
	{
		private ApplicationContext m_appContext = null;
		private DbSet<Type> m_daoSet = null;
		private Logger logger = null;

		public DatabaseDao(ApplicationContext appContext, DbSet<Type> daoSet)
		{
			logger = LogManager.GetCurrentClassLogger();
			m_appContext = appContext;
			m_daoSet = daoSet;
		}

		public List<Type> selectEntities()
		{
			try
			{
				List<Type> selectedEntities = m_daoSet.ToList();
				logger.Trace($"{typeof(Type).Name} selection is done correctly!");
				return selectedEntities;
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error($"Cannot select {typeof(Type).Name} entities");
			}
			return null;
		}

		public Type selectEntityById(int id)
		{
			Type entity = null;
			try {
				entity = m_daoSet.Where(e => e.getId() == id).Single();
				logger.Trace($"Selection {typeof(Type).Name} by id = {id} is done correctly!");
			} catch (System.InvalidOperationException) {
				logger.Error($"Cannot find {typeof(Type).Name} by id = {id}");
			}
			return entity;
		}

		public bool deleteEntityById(int id)
		{
			try
			{
				Type entity = selectEntityById(id);
				if (entity != null)
				{
					m_daoSet.Remove(entity);
					m_appContext.SaveChanges();
					logger.Trace($"{typeof(Type).Name} with id {id} is deleted!");
					return true;
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error($"Cannot delete {typeof(Type).Name} by id = {id}");
			}
			return false;
		}

		public int insertEntity(Type entity)
		{
			try
			{
				if (entity != null)
				{
					m_daoSet.Add(entity);
					m_appContext.SaveChanges();
					logger.Trace($"{typeof(Type).Name} with id = {entity.getId()} is added!");
					return entity.getId();
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error($"Cannot insert {typeof(Type).Name}");
			}
			return 0;
		}
	}
}
