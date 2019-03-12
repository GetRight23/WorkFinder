using System.Collections.Generic;

namespace Models
{
	public partial class Order : DBObject
	{
		public Order()
		{
			OrderToService = new HashSet<OrderToService>();
		}

		public int Id { get; set; }
		public string Info { get; set; }
		public int IdOrderList { get; set; }

		public override int getId() { return Id; }

		public virtual OrdersList IdOrderListNavigation { get; set; }
		public virtual ICollection<OrderToService> OrderToService { get; set; }
	}
}
