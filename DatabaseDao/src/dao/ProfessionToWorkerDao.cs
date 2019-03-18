using Microsoft.EntityFrameworkCore;
using Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace DatabaseDao
{
	public class ProfessionToWorkerDao
	{
		private DBContext dbContext = null;
		private DbSet<ProfessionToWorker> daoSet = null;
		private Logger logger = null;

		public ProfessionToWorkerDao(DBContext appContext, DbSet<ProfessionToWorker> daoSet)
		{
			dbContext = appContext;
			logger = LogManager.GetCurrentClassLogger();
			this.daoSet = daoSet;
		}

		public List<ProfessionToWorker> selectProfessionToWorkerByProfessionId(int id)
		{
			try
			{
				List<ProfessionToWorker> professionToWorkers = daoSet.Where(e => e.IdProfession == id).ToList();
				logger.Trace($"Profession to worker by profession id = {id} selection is done ");
				return professionToWorkers;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot select Profession to worker entities by profession id = {id}");
			}
			return null;
		}

		public List<ProfessionToWorker> selectProfessionToWorkerByWorkerId(int id)
		{
			try
			{
				List<ProfessionToWorker> professionToWorkers = daoSet.Where(e => e.IdWorker == id).ToList();
				logger.Trace($"Profession to worker by worker id = {id} selection is done");
				return professionToWorkers;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot select Profession to worker entities by worker id = {id}");
			}
			return null;
		}

		public List<int> selectWorkerIdsByProfessionId(int id)
		{
			try
			{
				List<ProfessionToWorker> professionToWorkers = daoSet.Where(e => e.IdProfession == id).ToList();
				List<int> workerIds = new List<int>();
				foreach (var elem in professionToWorkers)
				{
					workerIds.Add(elem.IdWorker);
				}
				logger.Trace($"Profession to worker selection by profession id = {id} is done");
				return workerIds;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot select Profession to worker by profession id = {id} entities");
			}
			return null;
		}

		public List<int> selectProfessionIdsByWorkerId(int id)
		{
			try
			{
				List<ProfessionToWorker> professionsToWorker = daoSet.Where(e => e.IdWorker == id).ToList();
				List<int> professionIds = new List<int>();
				foreach (var elem in professionsToWorker)
				{
					professionIds.Add(elem.IdProfession);
				}
				logger.Trace($"Profession to worker selection by worker id = {id} is done");
				return professionIds;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot select Profession to worker by worker id = {id} entities");
			}
			return null;
		}

		public bool removeProfessionByWorkerId(int id)
		{
			try
			{
				List<ProfessionToWorker> professionsToWorker = selectProfessionToWorkerByWorkerId(id);
				foreach (var elem in professionsToWorker)
				{
					daoSet.Remove(elem);
				}
				dbContext.SaveChanges();
				logger.Trace($"Profession to worker relationship remove with worker id = {id} is done");
				return true;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot remove relationship professions to worker with worker id = {id}");
			}
			return false;
		}

		public bool removeServicesByProfessionId(int id)
		{
			try
			{
				List<ProfessionToWorker> professionToWorkers = selectProfessionToWorkerByProfessionId(id);
				foreach (var elem in professionToWorkers)
				{
					daoSet.Remove(elem);
				}
				dbContext.SaveChanges();
				logger.Trace($"Profession to worker relationship remove with profession id = {id} is done");
				return true;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (SystemException ex)
			{
				logger.Error(ex.Message);
				logger.Error($"Cannot remove relationship profession to workers with profession id = {id}");
				
			}
			return false;
		}

		public int insertRelationship(int professionId, int workerId)
		{
			try
			{
				ProfessionToWorker professionToWorker = new ProfessionToWorker() { IdProfession = professionId, IdWorker = workerId };
				int id = 0;
				id = daoSet.Add(professionToWorker).Entity.Id;
				dbContext.SaveChanges();
				logger.Trace("Profession to worker insert is done");
				return id;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error("Cannot insert Profession to worker relationship");
			}
			return 0;
		}

		public int insertRelationship(ProfessionToWorker professionToWorker)
		{
			try
			{
				if (professionToWorker != null)
				{
					int id = 0;
					id = daoSet.Add(professionToWorker).Entity.Id;
					dbContext.SaveChanges();
					logger.Trace("Profession to worker insert is done");
					return id;
				}
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error("Cannot insert Profession to worker relationship");
			}
			return 0;
		}

		public bool updateEntity(ProfessionToWorker entity)
		{
			try
			{
				if (entity != null)
				{
					daoSet.Update(entity);
					dbContext.SaveChanges();
					logger.Trace($"Profession to worker with id {entity.Id} updated");
					return true;
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot update profession to worker with id {entity.Id}");
			}
			return false;
		}

		public List<int> insertRelationships(List<ProfessionToWorker> professionToWorkers)
		{
			try
			{
				if (professionToWorkers != null && professionToWorkers.Count != 0)
				{
					List<int> Ids = new List<int>();
					dbContext.Database.BeginTransaction();
					for (int i = 0; i < professionToWorkers.Count; i++)
					{
						int id = 0;
						id = daoSet.Add(professionToWorkers[i]).Entity.Id;
						dbContext.SaveChanges();
						Ids.Add(id);
					}
					dbContext.Database.CommitTransaction();
					logger.Trace($"Insert transaction Profession to worker is complited");
					return Ids;
				}
			}
			catch (TransactionException ex)
			{
				dbContext.Database.RollbackTransaction();
				logger.Error(ex.Message);
				logger.Error("Cannot begin insert Profession to worker transaction");
			}
			return null;
		}

		public bool updateEntities(List<ProfessionToWorker> entities)
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
					logger.Trace($"Update transaction Profession to worker is complited");
					return true;
				}
			}
			catch (TransactionException ex)
			{
				dbContext.Database.RollbackTransaction();
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot begin update Profession to worker transaction");
			}
			return false;
		}
	}
}
