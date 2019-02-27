using System;
using System.Collections.Generic;

namespace Models
{
    public partial class OrderTable
    {
        public OrderTable()
        {
            OrderToService = new HashSet<OrderToService>();
        }

        public int Id { get; set; }
        public string Info { get; set; }
        public int IdOrderList { get; set; }

        public virtual Orderslist IdOrderListNavigation { get; set; }
        public virtual ICollection<OrderToService> OrderToService { get; set; }
    }
}
