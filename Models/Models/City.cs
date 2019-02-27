using System;
using System.Collections.Generic;

namespace Models
{
    public partial class City
    {
        public City()
        {
            Address = new HashSet<Address>();
            CityDistricts = new HashSet<CityDistricts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<CityDistricts> CityDistricts { get; set; }
    }
}
