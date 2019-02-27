using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Worker
    {
        public Worker()
        {
            Feedback = new HashSet<Feedback>();
            Orderslist = new HashSet<Orderslist>();
            Profession = new HashSet<Profession>();
        }

        public int Id { get; set; }
        public double PhoneNumber { get; set; }
        public string Info { get; set; }
        public int IdAddress { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public virtual Address IdAddressNavigation { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<Orderslist> Orderslist { get; set; }
        public virtual ICollection<Profession> Profession { get; set; }
    }
}
