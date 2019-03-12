using System.Collections.Generic;

namespace Models
{
	public partial class OrdersList : DBObject
	{
		public OrdersList()
		{
			OrderTable = new HashSet<Order>();
		}

		public int Id { get; set; }
		public int IdWorker { get; set; }

		public override int getId() { return Id; }

		public virtual Worker IdWorkerNavigation { get; set; }
		public virtual ICollection<Order> OrderTable { get; set; }
	}
}
