using System;
using System.Collections.Generic;

namespace Models
{
	public partial class User : DBObject
	{
		public User()
		{
			Photo = new HashSet<Photo>();
		}

		public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public int IdWorker { get; set; }

		public override int getId() { return Id; }

		public virtual Worker IdWorkerNavigation { get; set; }
		public virtual ICollection<Photo> Photo { get; set; }
	}
}
