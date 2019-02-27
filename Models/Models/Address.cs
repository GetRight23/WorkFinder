﻿using System;
using System.Collections.Generic;

namespace Models/*
	{
		id: 1,

	}
	 
	 */
{
    public partial class Address
    {
        public Address()
        {
            Worker = new HashSet<Worker>();
        }

		public Address(string streetName, string apptNum, int idCityDistrict, int idCity)
		{
			StreetName = streetName;
			ApptNum = apptNum;
			IdCityDistrict = idCityDistrict;
			IdCity = idCity;

			Worker = new HashSet<Worker>();
		}

		public int Id { get; set; }
        public string StreetName { get; set; }
        public string ApptNum { get; set; }
        public int IdCityDistrict { get; set; }
        public int IdCity { get; set; }

        public virtual CityDistricts IdCityDistrictNavigation { get; set; }
        public virtual City IdCityNavigation { get; set; }
        public virtual ICollection<Worker> Worker { get; set; }
    }
}
