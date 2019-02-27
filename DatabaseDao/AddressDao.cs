using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace DatabaseDao
{
	public class AddressDao
	{
		private ApplicationContext storage = null;

		public AddressDao(ApplicationContext appContext)
		{
			storage = appContext;
		}

		public List<Address> selectAddreses()
		{
			List<Address> addresses = storage.Address.ToList();
			return addresses;
		}

		public Address selectAddressById(int id)
		{
			Address address = null;
			try {
				address = storage.Address.Where(e => e.Id == id).Single();
			} catch(System.InvalidOperationException) {
				Console.WriteLine($"Cannot find Address by id = {id}");
			}
			return address;
		}

		public void deleteAddressById(int id)
		{
			Address address = selectAddressById(id);
			if (address != null) 
			{
				storage.Address.Remove(address);
				storage.SaveChanges();
			}
		}

		public void insertAddress(Address address)
		{
			if (address != null) {
				storage.Address.Add(address);
				storage.SaveChanges();
			}
		}
	}
}
