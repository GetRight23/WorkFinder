using System;
using System.Collections.Generic;

namespace Models
{
	public partial class OrderToService
	{
		public int IdService { get; set; }
		public int IdOrder { get; set; }
		public int Id { get; set; }

		public virtual OrderTable IdOrderNavigation { get; set; }
		public virtual Service IdServiceNavigation { get; set; }
	}
}
