using System;
using System.Collections.Generic;

namespace Models
{
	public partial class Profession : DBObject
	{
		public Profession()
		{
			Service = new HashSet<Service>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public int IdWorker { get; set; }
		public int IdProfCategory { get; set; }

		public override int getId() { return Id; }

		public virtual ProfCategory IdProfCategoryNavigation { get; set; }
		public virtual Worker IdWorkerNavigation { get; set; }
		public virtual ICollection<Service> Service { get; set; }
	}
}
