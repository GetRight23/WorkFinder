using System;
using System.Collections.Generic;

namespace Models
{
	public partial class Worker : DBObject
	{
		public Worker()
		{
			Feedback = new HashSet<Feedback>();
			Orderslist = new HashSet<OrdersList>();
			ProfessionToWorker = new HashSet<ProfessionToWorker>();
		}

		public int Id { get; set; }
		public string PhoneNumber { get; set; }
		public string Info { get; set; }
		public int IdAddress { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public int IdUser { get; set; }

		public override int getId() { return Id; }

		public virtual Address IdAddressNavigation { get; set; }
		public virtual User IdUserNavigation { get; set; }
		public virtual ICollection<Feedback> Feedback { get; set; }
		public virtual ICollection<OrdersList> Orderslist { get; set; }
		public virtual ICollection<ProfessionToWorker> ProfessionToWorker { get; set; }
	}
}
