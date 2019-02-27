using System;
using System.Collections.Generic;

namespace Models
{
    public partial class CityDistricts
    {
        public CityDistricts()
        {
            Address = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdCity { get; set; }

        public virtual City IdCityNavigation { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}
