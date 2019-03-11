using System;
using System.Collections.Generic;

namespace Models
{
	public partial class Profession : DBObject
	{
		public Profession()
		{
			Service = new HashSet<Service>();
			ProfessionToWorker = new HashSet<ProfessionToWorker>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public int IdProfCategory { get; set; }

		public override int getId() { return Id; }

		public virtual ProfessionCategory IdProfCategoryNavigation { get; set; }
		public virtual ICollection<Service> Service { get; set; }
		public virtual ICollection<ProfessionToWorker> ProfessionToWorker { get; set; }
	}
}
