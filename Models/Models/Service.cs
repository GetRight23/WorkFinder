using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Service
    {
        public Service()
        {
            OrderToService = new HashSet<OrderToService>();
        }

        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int IdProffesion { get; set; }

        public virtual Profession IdProffesionNavigation { get; set; }
        public virtual ICollection<OrderToService> OrderToService { get; set; }
    }
}
