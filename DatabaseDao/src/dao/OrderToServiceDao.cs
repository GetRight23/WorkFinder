using System;
using Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using NLog;

namespace DatabaseDao
{
	public class OrderToServiceDao
	{
		private DBContext context = null;
		private DbSet<OrderToService> daoSet = null;
		private Logger logger = null;

		public OrderToServiceDao(DBContext context, DbSet<OrderToService> daoSet)
		{
			this.context = context;
			logger = LogManager.GetCurrentClassLogger();
			this.daoSet = daoSet;
		}

		public List<OrderToService> selectOrderToServicesByOrderId(int id)
		{
			try
			{
				List<OrderToService> ordersToServies = daoSet.Where(e => e.IdOrder == id).ToList();
				logger.Trace($"Order to service selection by order id = {id} is done");
				return ordersToServies;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot select Order to service entities by order id = {id}");
			}
			return null;
		}

		public List<OrderToService> selectOrderToServicesByServiceId(int id)
		{
			try
			{
				List<OrderToService> ordersToServies = daoSet.Where(e => e.IdService == id).ToList();
				logger.Trace($"Order to service selection by service id = {id} is done");
				return ordersToServies;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot select Order to service entities by service id = {id}");
			}
			return null;
		}

		public List<int> selectServiceIdsByOrderId(int id)
		{
			try
			{
				List<OrderToService> services = daoSet.Where(e => e.IdOrder == id).ToList();
				List<int> serviceIds = new List<int>();
				foreach (var elem in services)
				{
					serviceIds.Add(elem.IdService);
				}
				logger.Trace($"Order to service selection by order id = {id} is done");
				return serviceIds;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot select Order to service by order id = {id} entities");
			}
			return null;
		}

		public List<int> selectOrderIdsByServiceId(int id)
		{
			try
			{
				List<OrderToService> orders = daoSet.Where(e => e.IdService == id).ToList();
				List<int> orderIds = new List<int>();
				foreach (var elem in orders)
				{
					orderIds.Add(elem.IdOrder);
				}
				logger.Trace($"Order to service selection by service id = {id} is done");
				return orderIds;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot select Order to service by service id = {id} entities");
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
					daoSet.Remove(orderToService);
				}
				context.SaveChanges();
				logger.Trace($"Orders to service relationship remove with service id = {id} is done");
				return true;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot remove relationship orders to service with service id = {id}");
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
					daoSet.Remove(orderToService);
				}
				context.SaveChanges();
				logger.Trace($"Orders to service relationship remove with order id = {id} is done");
				return true;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error($"Cannot remove relationship order to services with order id = {id}");
			}
			return false;
		}

		public int insertRelationship(int orderId, int serviceId)
		{
			try
			{
				OrderToService orderToService = new OrderToService() { IdOrder = orderId, IdService = serviceId };
				int id = 0;
				id = daoSet.Add(orderToService).Entity.Id;
				context.SaveChanges();
				logger.Trace("Orders to service insert is done");
				return id;
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				logger.Error(ex.InnerException.Message);
				logger.Error("Cannot insert Order to service relationship");
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
					id = daoSet.Add(orderToService).Entity.Id;
					context.SaveChanges();
					logger.Trace("Orders to service insert is done");
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
				logger.Error("Cannot insert Order to service relationship");
			}
			return 0;
		}

		public bool updateEntity(OrderToService entity)
		{
			try
			{
				if (entity != null)
				{
					daoSet.Update(entity);
					context.SaveChanges();
					logger.Trace($"Order to service with id {entity.Id} updated");
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
				logger.Error($"Cannot update Order to service with id {entity.Id}");
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
					context.Database.BeginTransaction();
					for (int i = 0; i < ordersToServices.Count; i++)
					{
						int id = 0;
						id = daoSet.Add(ordersToServices[i]).Entity.Id;
						context.SaveChanges();
						Ids.Add(id);
					}
					context.Database.CommitTransaction();
					logger.Trace($"Insert transaction Order to service is complited");
					return Ids;
				}
			}
			catch (TransactionException ex)
			{
				context.Database.RollbackTransaction();
				logger.Error(ex.Message);
				logger.Error("Cannot begin insert Order to service transaction");
			}
			return null;
		}

		public bool updateEntities(List<OrderToService> entities)
		{
			try
			{
				if(entities != null && entities.Count != 0)
				{
					context.Database.BeginTransaction();
					foreach (var entity in entities)
					{
						updateEntity(entity);
					}
					context.Database.CommitTransaction();
					logger.Trace($"Update transaction Order to service is complited");
					return true;
				}
			}
			catch (TransactionException ex)
			{
				context.Database.RollbackTransaction();
				logger.Error(ex.Message);
				logger.Error("Cannot begin update Order to service transaction");
			}
			return false;
		}
	}
}
