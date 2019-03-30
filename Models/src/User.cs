using System.Collections.Generic;

namespace Models
{
	public partial class User : DBObject
	{
		public User()
		{
			
		}

		public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		public override int getId()
		{
			return Id;
		}

		public virtual Worker Worker { get; set; }
		public virtual Photo Photo { get; set; }
	}
}
