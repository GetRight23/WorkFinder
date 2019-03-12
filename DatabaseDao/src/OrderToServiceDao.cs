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
			try
			{
				List<OrderToService> ordersToServies = m_daoSet.Where(e => e.IdOrder == id).ToList();
				m_logger.Trace("Order to service selection is done");
				return ordersToServies;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.InnerException.Message);
				m_logger.Error("Cannot select Order to service entities");
			}
			return null;
		}

		public List<OrderToService> selectOrderToServicesByServiceId(int id)
		{
			try
			{
				List<OrderToService> ordersToServies = m_daoSet.Where(e => e.IdService == id).ToList();
				m_logger.Trace("Order to service selection is done");
				return ordersToServies;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.InnerException.Message);
				m_logger.Error("Cannot select Order to service entities");
			}
			return null;
		}

		public List<int> selectServiceIdsByOrderId(int id)
		{
			try
			{
				List<OrderToService> services = m_daoSet.Where(e => e.IdOrder == id).ToList();
				List<int> serviceIds = new List<int>();
				foreach (var elem in services)
				{
					serviceIds.Add(elem.IdService);
				}
				m_logger.Trace($"Order to service selection by order id = {id} is done");
				return serviceIds;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.InnerException.Message);
				m_logger.Error($"Cannot select Order to service by order id = {id} entities");
			}
			return null;
		}

		public List<int> selectOrderIdsByServiceId(int id)
		{
			try
			{
				List<OrderToService> orders = m_daoSet.Where(e => e.IdService == id).ToList();
				List<int> orderIds = new List<int>();
				foreach (var elem in orders)
				{
					orderIds.Add(elem.IdOrder);
				}
				m_logger.Trace($"Order to service selection by service id = {id} is done");
				return orderIds;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.InnerException.Message);
				m_logger.Error($"Cannot select Order to service by service id = {id} entities");
			}
			return null;
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
				m_logger.Trace($"Orders to service relationship remove with service id = {id} is done");
				return true;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.InnerException.Message);
				m_logger.Error($"Cannot remove relationship orders to service with service id = {id}");
			}
			return false;
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
				m_logger.Trace($"Orders to service relationship remove with order id = {id} is done");
				return true;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.InnerException.Message);
				m_logger.Error($"Cannot remove relationship order to services with order id = {id}");
			}
			return false;
		}

		public int insertRelationship(int orderId, int serviceId)
		{
			try
			{
				OrderToService orderToService = new OrderToService() { IdOrder = orderId, IdService = serviceId };
				int id = 0;
				id = m_daoSet.Add(orderToService).Entity.Id;
				m_dbContext.SaveChanges();
				m_logger.Trace("Orders to service insert is done");
				return id;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.InnerException.Message);
				m_logger.Error("Cannot insert Order to service relationship");
			}
			return 0;
		}

		public int insertRelationship(OrderToService orderToService)
		{
			try
			{
				if (orderToService != null)
				{
					int id = 0;
					id = m_daoSet.Add(orderToService).Entity.Id;
					m_dbContext.SaveChanges();
					m_logger.Trace("Orders to service insert is done");
					return id;
				}
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.InnerException.Message);
				m_logger.Error("Cannot insert Order to service relationship");
			}
			return 0;
		}

		public bool updateEntity(OrderToService entity)
		{
			try
			{
				if (entity != null)
				{
					m_daoSet.Update(entity);
					m_dbContext.SaveChanges();
					m_logger.Trace($"Order to service with id {entity.Id} updated");
					return true;
				}
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				m_logger.Error(ex.InnerException.Message);
				m_logger.Error($"Cannot update Order to service with id {entity.Id}");
			}
			return false;
		}

		public List<int> insertRelationships(List<OrderToService> ordersToServices)
		{
			try
			{
				if (ordersToServices != null && ordersToServices.Count != 0)
				{
					List<int> Ids = new List<int>();
					m_dbContext.Database.BeginTransaction();
					for (int i = 0; i < ordersToServices.Count; i++)
					{
						int id = 0;
						id = m_daoSet.Add(ordersToServices[i]).Entity.Id;
						m_dbContext.SaveChanges();
						Ids.Add(id);
					}
					m_dbContext.Database.CommitTransaction();
					m_logger.Trace($"Insert transaction Order to service is complited");
					return Ids;
				}
			}
			catch (TransactionException ex)
			{
				m_dbContext.Database.RollbackTransaction();
				m_logger.Error(ex.Message);
				m_logger.Error("Cannot begin insert Order to service transaction");
			}
			return null;
		}

		public bool updateEntities(List<OrderToService> entities)
		{
			try
			{
				if(entities != null && entities.Count != 0)
				{
					m_dbContext.Database.BeginTransaction();
					foreach (var entity in entities)
					{
						updateEntity(entity);
					}
					m_dbContext.Database.CommitTransaction();
					m_logger.Trace($"Update transaction Order to service is complited");
					return true;
				}
			}
			catch (TransactionException ex)
			{
				m_dbContext.Database.RollbackTransaction();
				m_logger.Error(ex.Message);
				m_logger.Error("Cannot begin update Order to service transaction");
			}
			return false;
		}
	}
}
