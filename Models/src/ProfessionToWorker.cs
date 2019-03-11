using System;
using System.Collections.Generic;

namespace Models
{
	public partial class ProfessionToWorker
	{
		public int Id { get; set; }
		public int IdProfession { get; set; }
		public int IdWorker { get; set; }

		public virtual Profession IdProfessionNavigation { get; set; }
		public virtual Worker IdWorkerNavigation { get; set; }
	}
}
