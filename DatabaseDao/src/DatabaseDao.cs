using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Transactions;

namespace DatabaseDao
{
	public class DatabaseDao<Type> : IDatabaseDao<Type> where Type : DBObject
	{
		private DBContext m_dbContext = null;
		private DbSet<Type> m_daoSet = null;
		private Logger m_logger = null;

		public DatabaseDao(DBContext appContext, DbSet<Type> daoSet)
		{
			m_logger = LogManager.GetCurrentClassLogger();
			m_dbContext = appContext;
			m_daoSet = daoSet;
		}

		public List<Type> selectEntities()
		{
			try
			{
				List<Type> selectedEntities = m_daoSet.ToList();
				m_logger.Trace($"{typeof(Type).Name} selection is done correctly!");
				return selectedEntities;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot select {typeof(Type).Name} entities");
			}
			return null;
		}

		public Type selectEntityById(int id)
		{
			Type entity = null;
			try
			{
				entity = m_daoSet.Where(e => e.getId() == id).Single();
				m_logger.Trace($"Selection {typeof(Type).Name} by id = {id} is done correctly!");
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (System.InvalidOperationException)
			{
				m_logger.Error($"Cannot find {typeof(Type).Name} by id = {id}");
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
					m_dbContext.SaveChanges();
					m_logger.Trace($"{typeof(Type).Name} with id {id} is deleted!");
					return true;
				}
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot delete {typeof(Type).Name} by id = {id}");
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
					m_dbContext.SaveChanges();
					m_logger.Trace($"{typeof(Type).Name} with id = {entity.getId()} is added!");
					return entity.getId();
				}
			}
			catch(TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot insert {typeof(Type).Name}");
			}
			return 0;
		}

		public bool updateEntity(Type entity)
		{
			try
			{
				if (entity != null)
				{
					m_daoSet.Update(entity);
					m_dbContext.SaveChanges();
					m_logger.Trace($"{typeof(Type).Name} with id {entity.getId()} is updated");
					return true;
				}
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot update {typeof(Type).Name} with id {entity.getId()}");
			}
			return false;
		}

		public List<int> insertEntities(List<Type> entities)
		{
			try
			{
				if (entities.Count != 0)
				{
					List<int> Ids = new List<int>();
					m_dbContext.Database.BeginTransaction();
					foreach (var entity in entities)
					{
						m_daoSet.Add(entity);
						m_dbContext.SaveChanges();

						Ids.Add(entity.getId());
					}
					m_dbContext.Database.CommitTransaction();

					m_logger.Trace($"Transaction {typeof(Type).Name} is complited");
					return Ids;
				}
			}
			catch (Exception ex)
			{
				m_dbContext.Database.RollbackTransaction();
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot begin insert {typeof(Type).Name} transaction");
			}
			return null;
		}

		public bool updateEntities(List<Type> entities)
		{
			try
			{
				m_dbContext.Database.BeginTransaction();
				foreach (var entity in entities)
				{
					updateEntity(entity);
				}
				m_dbContext.Database.CommitTransaction();
				return true;
			}
			catch (TransactionException ex)
			{
				m_dbContext.Database.RollbackTransaction();
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot begin update {typeof(Type).Name} transaction");
			}
			return false;
		}

		public List<Type> selectEntitiesByIds(List<int> ids)
		{
			List<Type> entities = new List<Type>();
			try
			{
				m_dbContext.Database.BeginTransaction();
				foreach (var id in ids)
				{
					entities.Add(selectEntityById(id));
				}
				m_dbContext.Database.CommitTransaction();
			}
			catch (TransactionException ex)
			{
				m_dbContext.Database.RollbackTransaction();
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot begin select {typeof(Type).Name} transaction");
			}
			return entities;
		}

		public bool deleteEntitiesByIds(List<int> ids)
		{
			try
			{
				bool state = true;
				m_dbContext.Database.BeginTransaction();
				foreach (var id in ids)
				{
					state = deleteEntityById(id);
				}
				m_dbContext.Database.CommitTransaction();
				return state;
			}
			catch (TransactionException ex)
			{
				m_dbContext.Database.RollbackTransaction();
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot begin delete {typeof(Type).Name} transaction");
				return false;
			}
		}
	}
}
