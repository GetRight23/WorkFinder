using Microsoft.EntityFrameworkCore;
using Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace DatabaseDao
{
	public class ProfessionToWorkerDao
	{
		private DBContext m_dbContext = null;
		private DbSet<ProfessionToWorker> m_daoSet = null;
		private Logger m_logger = null;

		public ProfessionToWorkerDao(DBContext appContext, DbSet<ProfessionToWorker> daoSet)
		{
			m_dbContext = appContext;
			m_logger = LogManager.GetCurrentClassLogger();
			m_daoSet = daoSet;
		}

		public List<ProfessionToWorker> selectProfessionToWorkerByProfessionId(int id)
		{
			return m_daoSet.Where(e => e.IdProfession == id).ToList();
		}

		public List<ProfessionToWorker> selectProfessionToWorkerByWorkerId(int id)
		{
			return m_daoSet.Where(e => e.IdWorker == id).ToList();
		}

		public List<int> selectWorkerIdsByProfessionId(int id)
		{
			List<ProfessionToWorker> professionToWorkers = m_daoSet.Where(e => e.IdProfession == id).ToList();
			List<int> workerIds = new List<int>();
			foreach (var elem in professionToWorkers)
			{
				workerIds.Add(elem.IdWorker);
			}
			return workerIds;
		}

		public List<int> selectProfessionIdsByWorkerId(int id)
		{
			List<ProfessionToWorker> professionsToWorker = m_daoSet.Where(e => e.IdWorker == id).ToList();
			List<int> professionIds = new List<int>();
			foreach (var elem in professionsToWorker)
			{
				professionIds.Add(elem.IdProfession);
			}
			return professionIds;
		}

		public bool removeProfessionByWorkerId(int id)
		{
			try
			{
				List<ProfessionToWorker> professionsToWorker = selectProfessionToWorkerByWorkerId(id);
				foreach (var elem in professionsToWorker)
				{
					m_daoSet.Remove(elem);
				}
				m_dbContext.SaveChanges();
				return true;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (SystemException)
			{
				m_logger.Error("Cannot remove relationship professions to worker");
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
					m_daoSet.Remove(elem);
				}
				m_dbContext.SaveChanges();
				return true;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (SystemException ex)
			{
				m_logger.Error(ex.Message);
				m_logger.Error("Cannot remove relationship profession to workers");
				
			}
			return false;
		}

		public int insertRelationship(int professionId, int workerId)
		{
			ProfessionToWorker professionToWorker = new ProfessionToWorker(){ IdProfession = professionId, IdWorker = workerId };
			int id = 0;
			id = m_daoSet.Add(professionToWorker).Entity.Id;
			m_dbContext.SaveChanges();
			return id;
		}

		public bool updateEntity(ProfessionToWorker entity)
		{
			try
			{
				if (entity != null)
				{
					m_daoSet.Update(entity);
					m_dbContext.SaveChanges();
					m_logger.Trace($"Profession to worker with id {entity.Id} updated");
					return true;
				}
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot update profession to worker with id {entity.Id}");
			}
			return false;
		}

		public bool updateEntities(List<ProfessionToWorker> entities)
		{
			try
			{
				if (entities != null && entities.Count != 0)
				{
					m_dbContext.Database.BeginTransaction();
					foreach (var entity in entities)
					{
						updateEntity(entity);
					}
					m_dbContext.Database.CommitTransaction();
					return true;
				}
			}
			catch (TransactionException ex)
			{
				m_dbContext.Database.RollbackTransaction();
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot begin update profession to worker transaction");
			}
			return false;
		}
	}
}
