using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Transactions;

namespace DatabaseDao
{
	public class DatabaseDaoImpl<Type> : DatabaseDao<Type> where Type : DBObject
	{
		private DBContext dbContext = null;
		private DbSet<Type> daoSet = null;
		private Logger logger = null;

		public DatabaseDaoImpl(DBContext appContext, DbSet<Type> daoSet)
		{
			logger = LogManager.GetCurrentClassLogger();
			dbContext = appContext;
			this.daoSet = daoSet;
		}

		public List<Type> selectEntities()
		{
			try
			{
				List<Type> selectedEntities = daoSet.ToList();
				logger.Trace($"{typeof(Type).Name} selection is done");
				return selectedEntities;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot select {typeof(Type).Name} entities");
			}
			return null;
		}

		public Type selectEntityById(int id)
		{
			try
			{
				Type entity = daoSet.Where(e => e.getId() == id).Single();
				logger.Trace($"Selection {typeof(Type).Name} by id = {id} is done");
				return entity;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				logger.Error($"Cannot find {typeof(Type).Name} by id = {id}");
			}
			return null;
		}

		public bool deleteEntityById(int id)
		{
			try
			{
				Type entity = selectEntityById(id);
				if (entity != null)
				{
					daoSet.Remove(entity);
					dbContext.SaveChanges();
					logger.Trace($"{typeof(Type).Name} with id {id} is deleted");
					return true;
				}
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
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
					daoSet.Add(entity);
					dbContext.SaveChanges();
					logger.Trace($"{typeof(Type).Name} with id = {entity.getId()} is added");
					return entity.getId();
				}
			}
			catch(TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot insert {typeof(Type).Name}");
			}
			return 0;
		}

		public bool updateEntity(Type entity)
		{
			try
			{
				if (entity != null)
				{
					daoSet.Update(entity);
					dbContext.SaveChanges();
					logger.Trace($"{typeof(Type).Name} with id {entity.getId()} is updated");
					return true;
				}
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot update {typeof(Type).Name} with id {entity.getId()}");
			}
			return false;
		}

		public List<int> insertEntities(List<Type> entities)
		{
			try
			{
				if (entities != null && entities.Count != 0)
				{
					List<int> Ids = new List<int>();
					dbContext.Database.BeginTransaction();
					foreach (var entity in entities)
					{
						daoSet.Add(entity);
						dbContext.SaveChanges();

						Ids.Add(entity.getId());
					}
					dbContext.Database.CommitTransaction();

					logger.Trace($"Insert transaction {typeof(Type).Name} is complited");
					return Ids;
				}
			}
			catch (TransactionException ex)
			{
				dbContext.Database.RollbackTransaction();
				logger.Error(ex.Message);
				logger.Error($"Cannot begin insert {typeof(Type).Name} transaction");
			}
			return null;
		}

		public bool updateEntities(List<Type> entities)
		{
			try
			{
				if (entities != null && entities.Count != 0)
				{
					dbContext.Database.BeginTransaction();
					foreach (var entity in entities)
					{
						updateEntity(entity);
					}
					dbContext.Database.CommitTransaction();

					logger.Trace($"Update transaction {typeof(Type).Name} is complited");
					return true;
				}
			}
			catch (TransactionException ex)
			{
				dbContext.Database.RollbackTransaction();
				logger.Error(ex.Message);
				logger.Error($"Cannot begin update {typeof(Type).Name} transaction");
			}
			return false;
		}

		public List<Type> selectEntitiesByIds(List<int> ids)
		{
			try
			{
				if (ids != null && ids.Count != 0)
				{
					List<Type> entities = new List<Type>();
					dbContext.Database.BeginTransaction();
					foreach (var id in ids)
					{
						entities.Add(selectEntityById(id));
					}
					dbContext.Database.CommitTransaction();

					logger.Trace($"Select by ids transaction {typeof(Type).Name} is complited");
					return entities;
				}
			}
			catch (TransactionException ex)
			{
				dbContext.Database.RollbackTransaction();
				logger.Error(ex.Message);
				logger.Error($"Cannot begin select by ids {typeof(Type).Name} transaction");
			}
			return null;
		}

		public bool deleteEntitiesByIds(List<int> ids)
		{
			try
			{
				if (ids != null && ids.Count != 0)
				{
					bool state = true;
					dbContext.Database.BeginTransaction();
					foreach (var id in ids)
					{
						state = deleteEntityById(id);
					}
					dbContext.Database.CommitTransaction();

					logger.Trace($"Delete by ids transaction {typeof(Type).Name} is complited");
					return state;
				}
			}
			catch (TransactionException ex)
			{
				dbContext.Database.RollbackTransaction();
				logger.Error(ex.Message);
				logger.Error($"Cannot begin delete by ids {typeof(Type).Name} transaction");
			}
			return false;
		}
	}
}
