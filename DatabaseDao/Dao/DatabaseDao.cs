using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseDao
{
	public class DatabaseDao<Type> where Type : DBObject
	{
		private ApplicationContext m_appContext = null;
		private DbSet<Type> m_daoSet = null;

		public DatabaseDao(ApplicationContext appContext, DbSet<Type> daoSet)
		{
			m_appContext = appContext;
			m_daoSet = daoSet;
		}

		public List<Type> selectEntities()
		{
			List<Type> selectedEntities = m_daoSet.ToList();
			return selectedEntities;
		}

		public Type selectEntityById(int id)
		{
			Type entity = null;
			try {
				entity = m_daoSet.Where(e => e.getId() == id).Single();
			} catch (System.InvalidOperationException) {
				Console.WriteLine($"Cannot find {typeof(Type).Name} by id = {id}");
			}
			return entity;
		}

		public bool deleteEntityById(int id)
		{
			Type entity = selectEntityById(id);
			if (entity != null)
			{
				m_daoSet.Remove(entity);
				m_appContext.SaveChanges();
				return true;
			}
			return false;
		}

		public bool insertEntity(Type entity)
		{
			if (entity != null)
			{
				m_daoSet.Add(entity);
				m_appContext.SaveChanges();
				return true;
			}
			return false;
		}
	}
}
