using System;
using Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DatabaseDao
{
	public class OrderToServiceDao
	{
		private ApplicationContext m_appContext = null;
		private DbSet<OrderToService> m_daoSet = null;

		public OrderToServiceDao(ApplicationContext appContext, DbSet<OrderToService> daoSet)
		{
			m_appContext = appContext;
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
				m_appContext.SaveChanges();
			}
			catch (SystemException)
			{
				Console.WriteLine("Cannot remove relationship orders to services");
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
				m_appContext.SaveChanges();
			}
			catch (SystemException ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine("Cannot remove relationship orders to services");
				return false;
			}
			return true;
		}

		public int insertRelationship(int orderId, int serviceId)
		{
			OrderToService orderToService = new OrderToService() { IdOrder = orderId, IdService = serviceId };
			int id = m_daoSet.Add(orderToService).Entity.Id;
			m_appContext.SaveChanges();
			return id;
		}
	}
}
