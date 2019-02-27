using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Orderslist : DBObject
	{
        public Orderslist()
        {
            OrderTable = new HashSet<OrderTable>();
        }

        public int Id { get; set; }
        public int IdWorker { get; set; }

		public override int getId() { return Id; }

		public virtual Worker IdWorkerNavigation { get; set; }
        public virtual ICollection<OrderTable> OrderTable { get; set; }
    }
}
