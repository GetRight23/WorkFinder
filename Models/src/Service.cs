using System.Collections.Generic;

namespace Models
{
	public partial class Service : DBObject
	{
		public Service()
		{
			OrderToService = new HashSet<OrderToService>();
		}

		public int Id { get; set; }
		public decimal Price { get; set; }
		public string Name { get; set; }
		public int IdProfession { get; set; }

		public override int getId() { return Id; }

		public virtual Profession IdProfessionNavigation { get; set; }
		public virtual ICollection<OrderToService> OrderToService { get; set; }
	}
}
