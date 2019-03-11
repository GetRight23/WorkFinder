using System;
using Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using NLog;

namespace DatabaseDao
{
	public class OrderToServiceDao
	{
		private DBContext m_dbContext = null;
		private DbSet<OrderToService> m_daoSet = null;
		private Logger m_logger = null;

		public OrderToServiceDao(DBContext appContext, DbSet<OrderToService> daoSet)
		{
			m_dbContext = appContext;
			m_logger = LogManager.GetCurrentClassLogger();
			m_daoSet = daoSet;
		}

		public List<OrderToService> selectOrderToServicesByOrderId(int id)
		{
			return m_daoSet.Where(e => e.IdOrder == id).ToList();
		}

		public List<OrderToService> selectOrderToServicesByServiceId(int id)
		{
			return m_daoSet.Where(e => e.IdService == id).ToList();
		}

		public List<int> selectServiceIdsByOrderId(int id)
		{
			List<OrderToService> services = m_daoSet.Where(e => e.IdOrder == id).ToList();
			List<int> serviceIds = new List<int>();
			foreach (var elem in services)
			{
				serviceIds.Add(elem.IdService);
			}
			return serviceIds;
		}

		public List<int> selectOrderIdsByServiceId(int id)
		{
			List<OrderToService> orders = m_daoSet.Where(e => e.IdService == id).ToList();
			List<int> orderIds = new List<int>();
			foreach (var elem in orders)
			{
				orderIds.Add(elem.IdService);
			}
			return orderIds;
		}

		public bool removeOrdersByServiceId(int id)
		{
			try
			{
				List<OrderToService> ordersIds = selectOrderToServicesByServiceId(id);
				OrderToService orderToService = new OrderToService();
				foreach (var elem in ordersIds)
				{
					orderToService = elem;
					m_daoSet.Remove(orderToService);
				}
				m_dbContext.SaveChanges();
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (SystemException)
			{
				m_logger.Error("Cannot remove relationship orders to services");
				return false;
			}
			return true;
		}

		public bool removeServicesByOrderId(int id)
		{
			try
			{
				List<OrderToService> servicesIds = selectOrderToServicesByOrderId(id);
				OrderToService orderToService = new OrderToService();
				foreach (var elem in servicesIds)
				{
					orderToService = elem;
					m_daoSet.Remove(orderToService);
				}
				m_dbContext.SaveChanges();
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (SystemException ex)
			{
				m_logger.Error(ex.Message);
				m_logger.Error("Cannot remove relationship orders to services");
				return false;
			}
			return true;
		}

		public int insertRelationship(int orderId, int serviceId)
		{
			OrderToService orderToService = new OrderToService() { IdOrder = orderId, IdService = serviceId };
			int id = m_daoSet.Add(orderToService).Entity.Id;
			m_dbContext.SaveChanges();
			return id;
		}

		public void updateEntity(OrderToService entity)
		{
			try
			{
				if (entity != null)
				{
					m_daoSet.Update(entity);
					m_dbContext.SaveChanges();
					m_logger.Trace($"OrderToService with id {entity.Id} updated");
				}
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot update OrderToService with id {entity.Id}");
			}
		}

		public void updateEntities(List<OrderToService> entities)
		{
			try
			{
				m_dbContext.Database.BeginTransaction();
				foreach (var entity in entities)
				{
					updateEntity(entity);
				}
				m_dbContext.Database.CommitTransaction();
			}
			catch (TransactionException ex)
			{
				m_dbContext.Database.RollbackTransaction();
				m_logger.Error(ex.Message);
				m_logger.Error($"Cannot begin update OrderToService transaction");
			}
		}
	}
}
